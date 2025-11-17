document.querySelectorAll(".btn.cart").forEach((btn, index) => {
    btn.addEventListener("click", function () {

        // Fake data mỗi sản phẩm (tùy card index)
        let product = {
            id: index,
            name: "Sản phẩm " + (index + 1),
            price: 100000, // giá demo
            quantity: 1,
            img: "img/product" + (index + 1) + ".jpg"
        };

        let cart = JSON.parse(localStorage.getItem("cart")) || [];

        // kiểm tra trùng id → tăng số lượng
        let exist = cart.find(p => p.id === product.id);
        if (exist) {
            exist.quantity++;
        } else {
            cart.push(product);
        }

        localStorage.setItem("cart", JSON.stringify(cart));
        alert("Đã thêm vào giỏ!");

    });
});


