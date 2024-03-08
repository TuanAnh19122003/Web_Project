// Trong file index.js
let currentIndex = 0;
const totalImages = document.querySelectorAll('.list-images img').length;
const imageContainer = document.querySelector('.list-images');

function showNextImage() {
    currentIndex = (currentIndex + 1) % totalImages;
    updateSlideShow();
}

function updateSlideShow() {
    const translateValue = -currentIndex * 100;
    imageContainer.style.transition = 'transform 0.5s ease-in-out'; // Thêm dòng này
    imageContainer.style.transform = `translateX(${translateValue}%)`;

    if (currentIndex === totalImages - 1) {
        setTimeout(() => {
            currentIndex = 0;
            imageContainer.style.transition = 'none';
            updateSlideShow();
        }, 500);
    }
}

setInterval(showNextImage, 5000);
