
$(function () {
    function countCartTotal() {
        let cartTotal = 0;
        cart.forEach(cartItem => {
            cartTotal += parseInt(cartItem.quantity) * parseInt(cartItem.price);
        });
        if (cartTotal !== 0) {
            $(".totalPrice span").show();

            document.querySelector(".totalPrice span").innerText = cartTotal + " Azn";
        } else {
            $(".totalPrice").hide();
        }
    }

    let cart = JSON.parse(localStorage.getItem('cart')) || [];
    $(window).on('load', () => {
        if (cart.length === 0) {
            setTimeout(function () {
                window.location.href = "/Home/Index";
            }, 1500);
        }
    });

    const cartCheckOutDom = document.querySelector('.checkout-modal');
    cart.forEach(cartItem => {
        let product = cartItem;
        if (product !== null) {
            $(".Ptext").remove();
            cartCheckOutDom.insertAdjacentHTML('beforeend', `
              <div class="cart_item">
              <img name="Image" class="cart_item_img" src="${product.image}" alt="${product.name}">
              <p name="Name" class="cart_item_name">${product.name}</p>
              <h4 name="Price" class="cart_item_price">${(product.totalPrice === 0 ? product.price : product.totalPrice)} </h4>
                <div class="quantity_items">
                     <input class="col-sm col-md-5" min="1" type='number' value='${product.quantity}' name='quantity'/>
                </div>
              <button class="btn btn-danger btn-sm" data-action="Remove_Item">&times;</button>
            </div>
          `);
            countCartTotal();
            const cartItemsDOM = cartCheckOutDom.querySelectorAll('.cart_item');
            cartItemsDOM.forEach(cartItemDOM => {
                if (cartItemDOM.querySelector('.cart_item_name').innerText.toLowerCase().trim() === product.name.toLowerCase().trim()) {
                    cartItemDOM.querySelector('[data-action="Remove_Item"]').addEventListener('click', () => removeItem(product, cartItemDOM));
                }
            });

        }

    });
    $(".btnCheck").click(function () {
        checkout();
    });
    function checkout() {
        cart.forEach(cartItem => {
            $.ajax({
                url: "https://texnogallery.az/OrdersProduct/Index/" + cartItem.pId,
                type: "POST",
                dataType: "json",
                data: { 'quantity': cartItem.quantity },
                success: function (res) {
                    if (res.status === 200) {

                        cartCheckOutDom.querySelectorAll('.cart_item').forEach(cartItemDOM => {
                            cartItemDOM.classList.add('cart_item_removed');
                            setTimeout(() => cartItemDOM.remove(), 250);
                        });

                        cart = [];
                        localStorage.removeItem('cart');

                        $(".btnCheck").remove();
                        $(".btnH").remove();
                        if (!$(".checkout-modal").hasClass("successCheck")) {
                            $(".checkout-modal").addClass("successCheck");
                            cartCheckOutDom.insertAdjacentHTML("beforeend", `
                                <div class="jumbotron text-center">
                                  <h1 class="display-3 text-success">Təşəkkürlər!</h1>
                                  <p class="lead"><b>Sifarişiniz</b> qəbul olundu.Tezliklə sizinlə əlaqə saxlanılacaq.</p>
                                  <hr>
                                  <p class="lead">
                                    <a class="btn btn-outline-primary btn-lg" href="/Home" role="button">Ana Səhifə</a>
                                  </p>
                                </div>
                        `);
                        }
                    }
                }

            });
        });


    }
    function removeItem(product, cartItemDOM) {
        cartItemDOM.classList.add('cart_item_removed');
        setTimeout(() => cartItemDOM.remove(), 250);
        cart = cart.filter(cartItem => cartItem.name !== product.name);
        localStorage.setItem('cart', JSON.stringify(cart));
        countCartTotal();
        //addToCartButtonDOM.innerText = 'Səbətə Əlavə Et';
        //addToCartButtonDOM.disabled = false;

        if (cart.length < 1) {
            document.querySelector('.cart-footer').remove();
        }
    }


});
 
document.getElementById('cart').addEventListener('click',
    function () {
        document.querySelector('.bg-modal').style.display = 'flex';
        $("body").addClass("modalActive");
    });

document.querySelector('.close-modal').addEventListener('click',
    function () {
        document.querySelector('.bg-modal').style.display = 'none';
        $("body").removeClass("modalActive");

    }); 