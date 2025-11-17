
    //const selectAll = document.getElementById('selectAll');
    //const totalPriceEl = document.getElementById('total-price');
    //const finalPriceEl = document.getElementById('final-price');

    //function formatCurrency(num) {
    //    return num.toLocaleString('vi-VN') + '₫';
    //}

    //// 👉 Hàm lấy giá số (từ text "11.490.000đ")
    //function toNumber(text) {
    //    return parseInt(text.replace(/\D/g, ''));
    //}

    //// 👉 Hàm cập nhật tổng
    //function updateTotal() {
    //    let items = document.querySelectorAll('.cart-item'); // linh hoạt
    //let total = 0;

    //    items.forEach(item => {
    //        const check = item.querySelector('.item-check');
    //const qty = parseInt(item.querySelector('.quantity').textContent);
    //const price = toNumber(item.querySelector('.price').textContent);

    //if (check.checked) {
    //    total += price * qty;
    //        }
    //    });

    //totalPriceEl.textContent = formatCurrency(total);
    //finalPriceEl.textContent = formatCurrency(total);
    //}

    //// ✅ Chọn tất cả
    //selectAll.addEventListener('change', () => {
    //    document.querySelectorAll('.item-check')
    //        .forEach(ch => ch.checked = selectAll.checked);
    //updateTotal();
    //});

    //// ✅ Tick từng item
    //document.addEventListener('change', e => {
    //    if (e.target.classList.contains('item-check')) {
    //    updateTotal();
    //    }
    //});

    //// ✅ Nút cộng
    //document.addEventListener('click', e => {
    //    if (e.target.classList.contains('plus')) {
    //        const qtyEl = e.target.parentElement.querySelector('.quantity');
    //qtyEl.textContent = parseInt(qtyEl.textContent) + 1;
    //updateTotal();
    //    }
    //});

    //// ✅ Nút trừ
    //document.addEventListener('click', e => {
    //    if (e.target.classList.contains('minus')) {
    //        const qtyEl = e.target.parentElement.querySelector('.quantity');
    //let q = parseInt(qtyEl.textContent);
    //        if (q > 1) {
    //    qtyEl.textContent = q - 1;
    //updateTotal();
    //        }
    //    }
    //});

    //// ✅ Xóa sản phẩm
    //document.addEventListener('click', e => {
    //    if (e.target.classList.contains('remove')) {
    //    e.target.closest('.cart-item').remove();
    //updateTotal();
    //    }
    //});

//updateTotal();
// DOM
const selectAll = document.getElementById('selectAll');
const totalPriceEl = document.getElementById('total-price');
const discountEl = document.getElementById('discount');
const finalPriceEl = document.getElementById('final-price');
const checkoutBtn = document.querySelector('.checkout-btn');


// Convert 11.490.000₫ → 11490000
function toNumber(text) {
    return parseInt(text.replace(/\D/g, '')) || 0;
}

// Format tiền
function formatCurrency(num) {
    return num.toLocaleString('vi-VN') + '₫';
}


// ✅ Cập nhật tổng
function updateTotal() {
    let items = document.querySelectorAll('.cart-item');
    let total = 0;
    let discount = 0;

    items.forEach(item => {
        const check = item.querySelector('.item-check');
        const qty = parseInt(item.querySelector('.quantity').textContent);
        const newPrice = toNumber(item.querySelector('.new-price').textContent);
        const oldPrice = toNumber(item.querySelector('.old-price').textContent);

        if (check.checked) {
            total += newPrice * qty;
            discount += (oldPrice - newPrice) * qty;
        }
    });

    totalPriceEl.textContent = formatCurrency(total);
    discountEl.textContent = "-" + formatCurrency(discount);
    finalPriceEl.textContent = formatCurrency(total - discount);

    // Disable khi không tick gì
    checkoutBtn.disabled = total === 0;
}



// ✅ Chọn tất cả
selectAll.addEventListener('change', () => {
    document.querySelectorAll('.item-check')
        .forEach(ch => ch.checked = selectAll.checked);
    updateTotal();
});


// ✅ Tick từng item
document.addEventListener('change', e => {
    if (e.target.classList.contains('item-check')) {
        let all = document.querySelectorAll('.item-check');
        let checked = document.querySelectorAll('.item-check:checked');

        selectAll.checked = all.length === checked.length;
        updateTotal();
    }
});


// ✅ Nút cộng
document.addEventListener('click', e => {
    if (e.target.classList.contains('plus')) {
        const qtyEl = e.target.parentElement.querySelector('.quantity');
        qtyEl.textContent = parseInt(qtyEl.textContent) + 1;
        updateTotal();
    }
});


// ✅ Nút trừ
document.addEventListener('click', e => {
    if (e.target.classList.contains('minus')) {
        const qtyEl = e.target.parentElement.querySelector('.quantity');
        let q = parseInt(qtyEl.textContent);
        if (q > 1) {
            qtyEl.textContent = q - 1;
            updateTotal();
        }
    }
});


// ✅ Xóa sản phẩm
document.addEventListener('click', e => {
    if (e.target.classList.contains('remove')) {
        e.target.closest('.cart-item').remove();
        updateTotal();
    }
});


// ✅ Thanh toán
checkoutBtn.addEventListener('click', () => {
    alert("✅ Bạn đã xác nhận đơn hàng!");
});

// Load lần đầu
updateTotal();

