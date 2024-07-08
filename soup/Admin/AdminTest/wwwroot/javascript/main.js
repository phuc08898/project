var btnShows = document.querySelectorAll(".js-btn-show-menu");
var modelMenu = document.querySelectorAll('.js-show-menu-icon-hide');
var icons = document.querySelectorAll('.icon-size');
var iconhide = document.querySelectorAll('.icon-hide');

function toggleMenu(event) {
    var clickedButton = event.currentTarget;
    var correspondingMenu = clickedButton.nextElementSibling;

    correspondingMenu.classList.toggle('open');
}

for (var i = 0; i < btnShows.length; i++) {
    btnShows[i].addEventListener('click', toggleMenu);
}


// Lấy tất cả các nút có class 'js-btn-show-menu'
var btnShows = document.querySelectorAll('.js-btn-show-menu');

// Lấy tất cả các biểu tượng có class 'icon-size' và 'icon-hide'
var icons = document.querySelectorAll('.icon-size');
var iconhides = document.querySelectorAll('.icon-hide');

// Lặp qua từng nút và thêm sự kiện click
btnShows.forEach(function (btnShow, index) {
    btnShow.addEventListener('click', function () {
        // Ẩn/hiện các biểu tượng tương ứng với nút được click
        icons[index].classList.toggle('icon-hide');
        iconhides[index].classList.toggle('show-icon');
        // Thêm chuyển động quay tròn
        icons[index].classList.toggle('rotate-360');
        iconhides[index].classList.toggle('rotate-360');
    });
});

// js cua
// Get all the toggle switches
const toggleSwitches = document.querySelectorAll('.toggle-switch-checkbox');
for (var i = 0; i < toggleSwitches.length; i++) {
    console.log(toggleSwitches)
}
/*js add img*/
const imageInput = document.querySelector('.image-input');
const imageInputWrapper = document.querySelector('.image-input-wrapper');
const productImageContainer = document.querySelector('.product-image-container');
const maxImageCount = 5;

let images = [];

function handleFileChange(event) {
    const files = event.target.files;
    for (let i = 0; i < files.length && images.length < maxImageCount; i++) {
        const file = files[i];
        const imageWrapper = createImageWrapper(file);
        productImageContainer.insertBefore(imageWrapper, imageInputWrapper);
        images.push({
            url: URL.createObjectURL(file),
            altText: `Product Image ${images.length + 1}`
        });
    }
}

function createImageWrapper(file) {
    const imageWrapper = document.createElement('div');
    imageWrapper.classList.add('product-image-wrapper');
    const image = document.createElement('img');
    image.src = URL.createObjectURL(file);
    image.alt = `Product Image ${images.length + 1}`;
    image.classList.add('product-image');
    const removeButton = document.createElement('button');
    removeButton.classList.add('remove-image');
    removeButton.textContent = 'Xóa';
    removeButton.addEventListener('click', () => {
        removeImage(image.alt);
        imageWrapper.remove();
    });
    imageWrapper.appendChild(image);
    imageWrapper.appendChild(removeButton);
    return imageWrapper;
}

function removeImage(altText) {
    const index = images.findIndex(image => image.altText === altText);
    if (index !== -1) {
        images.splice(index, 1);
    }
}

imageInputWrapper.addEventListener('click', () => {
    imageInput.click();
});

imageInput.addEventListener('change', handleFileChange);
/*phần sreach */
document.getElementById('searchForm').addEventListener('submit', function (event) {
    event.preventDefault();


    const searchQuery = document.getElementById('searchQuery').value;
    const notification = document.getElementById('notification');
    const resultsContainer = document.getElementById('results');

    // Reset notification and results
    notification.style.display = 'none';
    resultsContainer.innerHTML = '';

    // Gửi yêu cầu GET đến API server với query tìm kiếm
    fetch(`http://localhost:5005/api/v1/?name=${encodeURIComponent(searchQuery)}`) // Thay thế URL này bằng URL API 
        .then(response => {
            if (!response.ok) {
                throw new Error('Tìm kiếm sản phẩm không thành công');
            }
            return response.json();
        })
        .then(data => {
            if (data.length === 0) {
                notification.textContent = 'Không tìm thấy sản phẩm';
                notification.className = 'notification error';
                notification.style.display = 'block';
            } else {
                data.forEach(product => {
                    const productItem = document.createElement('div');
                    productItem.className = 'result-item';
                    productItem.textContent = `Tên: ${product.name}, Giá: ${product.price}`;
                    resultsContainer.appendChild(productItem);
                });
            }
        })
        .catch(error => {
            notification.textContent = error.message;
            notification.className = 'notification error';
            notification.style.display = 'block';
        });
});
function showNextPage() {
    var dropdownContent = document.getElementsByClassName("dropdown-content")[0];
    var items = dropdownContent.getElementsByTagName("p");
    var startIndex = 0;
    var endIndex = 10;

    for (var i = 0; i < items.length; i++) {
        items[i].style.display = "none";
    }

    for (var i = startIndex; i < endIndex && i < items.length; i++) {
        items[i].style.display = "block";
    }

    startIndex += 5;
    endIndex += 5;
}
function validatePrice() {
    var minPrice = document.getElementById("minPrice").value;
    var maxPrice = document.getElementById("maxPrice").value;

    // Kiểm tra xem các giá trị đã nhập có phải là số hay không
    if (isNaN(minPrice) || isNaN(maxPrice)) {
        alert("Vui lòng chỉ nhập số!");
        return false;
    }

    // Nếu các giá trị hợp lệ, thực hiện các hành động khác
    console.log("Giá từ " + minPrice + " đến " + maxPrice);
    return true;
}

// Hàm này sẽ cấm người dùng nhập chữ vào các trường input
function preventTextInput(event) {
    if (!/\d/.test(event.key)) {
        event.preventDefault();
    }
}
