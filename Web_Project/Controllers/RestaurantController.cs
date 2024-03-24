using PagedList;
using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_Project.Models;

namespace Web_Project.Controllers
{
    public class RestaurantController : Controller
    {
        private Restaurant_Web_DataEntities db = new Restaurant_Web_DataEntities();
        // GET: Restaurant
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AboutUs()
        {
            return View();
        }
        public ActionResult Contact([Bind(Include = "id,name,email,phone_number,request_date,Note")] Contact ct)
        {
            if (ModelState.IsValid)
            {
                db.Contacts.Add(ct);
                db.SaveChanges();
                return RedirectToAction("Contact");
            }

            ModelState.Clear();
            return View(ct);

        }
        public ActionResult Menu()
        {
            var menuData = db.Menus.Include("Category").ToList();
            return View(menuData);
        }
        public ActionResult Deals(int ? page)
        {
            int pageSize = 6;
            int pageNumber = (page ?? 1);
            var list = db.Deals.OrderBy(b => b.id).ToPagedList(pageNumber, pageSize);
            return View(list);
        }
        public ActionResult Reservation([Bind(Include = "reservation_id,customer_name,phone_number,table_id,quantity,reservation_time,reservation_date,Note")] Reservation res)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Dashboard", new { area = "Admin" });
            }
            else
            {
                if (ModelState.IsValid)
                {
                    db.Reservations.Add(res);
                    db.SaveChanges();
                    return RedirectToAction("ReservationPaymentWithPaypal");
                }
                ModelState.Clear();
                ViewBag.table_id = new SelectList(db.Table_Res, "id", "name");
                return View(res);
            }
        }
        private PayPal.Api.Payment payment;
        private Payment ExecutePayment(APIContext apiContext, string payerId, string paymentId)
        {
            var paymentExecution = new PaymentExecution()
            {
                payer_id = payerId
            };
            this.payment = new Payment()
            {
                id = paymentId
            };
            return this.payment.Execute(apiContext, paymentExecution);
        }

        public ActionResult ReservationPaymentWithPaypal(string Cancel = null)
        {
            //getting the apiContext
            APIContext apiContext = PaypalConfiguration.GetAPIContext();
            try
            {
                //A resource representing a Payer that funds a payment Payment Method as paypal
                //Payer Id will be returned when payment proceeds or click to pay
                string payerId = Request.Params["PayerID"];
                if (string.IsNullOrEmpty(payerId))
                {
                    //this section will be executed first because PayerID doesn't exist
                    //it is returned by the create function call of the payment class
                    // Creating a payment
                    // baseURL is the url on which paypal sendsback the data.
                    string baseURI = Request.Url.Scheme + "://" + Request.Url.Authority + "/Restaurant/ReservationPaymentWithPaypal?";
                    //here we are generating guid for storing the paymentID received in session
                    //which will be used in the payment execution
                    var guid = Convert.ToString((new Random()).Next(100000));
                    //CreatePayment function gives us the payment approval url
                    //on which payer is redirected for paypal account payment
                    var createdPayment = this.CreateReservationPayment(apiContext, baseURI + "guid=" + guid, "Reservation", "USD", 50);
                    //get links returned from paypal in response to Create function call
                    var links = createdPayment.links.GetEnumerator();
                    string paypalRedirectUrl = null;
                    while (links.MoveNext())
                    {
                        Links lnk = links.Current;
                        if (lnk.rel.ToLower().Trim().Equals("approval_url"))
                        {
                            //saving the payapalredirect URL to which user will be redirected for payment
                            paypalRedirectUrl = lnk.href;
                        }
                    }
                    // saving the paymentID in the key guid
                    Session.Add(guid, createdPayment.id);
                    return Redirect(paypalRedirectUrl);
                }
                else
                {
                    // This function exectues after receving all parameters for the payment
                    var guid = Request.Params["guid"];
                    var executedPayment = ExecutePayment(apiContext, payerId, Session[guid] as string);
                    //If executed payment failed then we will show payment failure message to user
                    if (executedPayment.state.ToLower() != "approved")
                    {
                        return View("FailureView");
                    }
                }
            }
            catch (Exception ex)
            {
                return View("FailureView");
            }
            //on successful payment, show success page to user.
            return View("SuccessView");
        }
        public ActionResult SuccessView()
        {
            return View();
        }
        public ActionResult FailureView()
        {
            return View();
        }
        private Payment CreateReservationPayment(APIContext apiContext, string redirectUrl, string reservationName, string currency, decimal totalPrice)
        {
            // Tạo danh sách các sản phẩm
            var itemList = new ItemList()
            {
                items = new List<Item>()
                {
                    new Item()
                    {
                        name = reservationName, // Đổi tên sản phẩm thành tên đặt bàn
                        currency = currency, // Đổi tiền tệ thành tiền tệ mong muốn (VD: "USD", "VND")
                        price = totalPrice.ToString(), // Đặt giá sản phẩm là tổng giá tiền
                        quantity = "1", // Số lượng là 1 vì chỉ có một đặt bàn
                        sku = "reservation"
                    }
                }
            };

            // Tạo đối tượng Amount với tổng số tiền
            var amount = new Amount()
            {
                currency = currency, // Tiền tệ của tổng số tiền
                total = totalPrice.ToString(), // Tổng số tiền thanh toán
                details = new Details()
                {
                    tax = "0", // Thuế, có thể tính toán tùy theo yêu cầu
                    shipping = "0", // Phí vận chuyển, có thể tính toán tùy theo yêu cầu
                    subtotal = totalPrice.ToString() // Tổng số tiền chưa bao gồm thuế và phí vận chuyển
                }
            };

            // Tạo giao dịch với thông tin sản phẩm
            var transactionList = new List<Transaction>();
            var paypalOrderId = DateTime.Now.Ticks;
            transactionList.Add(new Transaction()
            {
                description = $"Invoice #{paypalOrderId}",
                invoice_number = paypalOrderId.ToString(), // Số hóa đơn (có thể thay đổi tùy theo yêu cầu)
                amount = amount,
                item_list = itemList
            });

            // Tạo đối tượng Payment
            var payment = new Payment()
            {
                intent = "sale",
                payer = new Payer() { payment_method = "paypal" },
                transactions = transactionList,
                redirect_urls = new RedirectUrls()
                {
                    cancel_url = redirectUrl + "&Cancel=true",
                    return_url = redirectUrl
                }
            };

            // Tạo thanh toán và trả về đối tượng Payment
            return payment.Create(apiContext);
        }
    }
}