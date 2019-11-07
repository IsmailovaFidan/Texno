
/*****************************************/
'use strict';

let cart = JSON.parse(localStorage.getItem('cart')) || [];
const cartDOM = document.querySelector('.modal-item');
const addToCartButtonsDOM = document.querySelectorAll('[data-action="Add_To_Cart"]');
if (cart.length > 0) {
    $(".modal-item").empty();
    cart.forEach(cartItem => {
        const product = cartItem;
        insertItemToDOM(product);
        countCartTotal();
        addToCartButtonsDOM.forEach(addToCartButtonDOM => {
            const productDOM = addToCartButtonDOM.parentNode.parentNode;
            if (productDOM.querySelector('.pro_name').innerText.trim() === product.name.trim()) {
                handleActionButtons(addToCartButtonDOM, product);
            }
        });

     });
}

addToCartButtonsDOM.forEach(addToCartButtonDOM => {
    addToCartButtonDOM.addEventListener('click', () => {
        const productDOM = addToCartButtonDOM.parentNode.parentNode;
        const product = {
            image: productDOM.querySelector('.pro_image').getAttribute('src'),
            name: productDOM.querySelector('.pro_name').innerText,
            price: productDOM.querySelector('.pro_price').innerText,
            quantity: 1,
            pId: productDOM.querySelector(".hidePId").getAttribute('value'),
            totalPrice:0
        };
        const isInCart = cart.filter(cartItem => cartItem.name.trim() === product.name.trim()).length > 0;

        if (!isInCart) {
            insertItemToDOM(product);
            cart.push(product);
            localStorage.setItem('cart', JSON.stringify(cart));
            countCartTotal();
            handleActionButtons(addToCartButtonDOM, product);
            countProductTotal();

        }
    });
});
//handleActionButtons(addToCartButtonsDOM, product)



function insertItemToDOM(product) {
    cartDOM.insertAdjacentHTML('beforeend', `
    <div class="cart_item">
        <input type="hidden" value=${product.pId}">
      <img class="cart_item_img" src="${product.image}" alt="${product.name}">
      <p class="cart_item_name">${product.name}</p>
      <h4 class="cart_item_price">${(product.totalPrice === 0 ? product.price : product.totalPrice )}</h4>
        <div class="quantity_items">
              <button class="btn  btn-primary btn-sm${(product.quantity === 1 ? ' btn-danger' : '')}" data-action="Decrease_Item">&minus;</button>
              <h5 class="cart_item_quantity">${product.quantity}</h5>
              <button class="btn  btn-primary btn-sm" data-action="Increase_Item">&plus;</button>
        </div>
      <button class="btn btn-danger btn-sm" data-action="Remove_Item">&times;</button>

    </div>
  `);
    addCartFooter();

}
cart.forEach(cartItem => {

    var product = cartItem;

    const cartItemsDOM = cartDOM.querySelectorAll('.cart_item');
    cartItemsDOM.forEach(cartItemDOM => {
        if (cartItemDOM.querySelector('.cart_item_name').innerText.toLowerCase().trim() === product.name.toLowerCase().trim()) {
            cartItemDOM.querySelector('[data-action="Increase_Item"]').addEventListener('click', () => increaseItem(product, cartItemDOM));
            cartItemDOM.querySelector('[data-action="Decrease_Item"]').addEventListener('click', () => decreaseItem(product, cartItemDOM));
            cartItemDOM.querySelector('[data-action="Remove_Item"]').addEventListener('click', () => removeItem(product, cartItemDOM));
        }
    });

});

function handleActionButtons(addToCartButtonDOM, product) {
    addToCartButtonDOM.innerText = 'Səbətə Əlavə Edildi';
    addToCartButtonDOM.disabled = true;
    const cartItemsDOM = cartDOM.querySelectorAll('.cart_item');
    cartItemsDOM.forEach(cartItemDOM => {
        if (cartItemDOM.querySelector('.cart_item_name').innerText.toLowerCase().trim() === product.name.toLowerCase().trim()) {
            cartItemDOM.querySelector('[data-action="Increase_Item"]').addEventListener('click', () => increaseItem(product, cartItemDOM));
            cartItemDOM.querySelector('[data-action="Decrease_Item"]').addEventListener('click', () => decreaseItem(product, cartItemDOM));
            cartItemDOM.querySelector('[data-action="Remove_Item"]').addEventListener('click', () => removeItem(product, cartItemDOM));
        }
    });
}

function increaseItem(product, cartItemDOM) {
    cart.forEach(cartItem => {

        if (cartItem.name === product.name) {
            cartItemDOM.querySelector('.cart_item_quantity').innerText = ++cartItem.quantity;
            cartItem.totalPrice = parseInt(cartItem.price) * cartItem.quantity;
            
            cartItemDOM.querySelector(".cart_item_price").innerText = cartItem.totalPrice+" Azn";

            cartItemDOM.querySelector('[data-action="Decrease_Item"]').classList.remove('btn-danger');
            localStorage.setItem('cart', JSON.stringify(cart));
            countCartTotal();
        }
    });
}

function decreaseItem(product, cartItemDOM, addToCartButtonDOM) {
    cart.forEach(cartItem => {
        if (cartItem.name === product.name) {
            if (cartItem.quantity > 1) {
                cartItemDOM.querySelector('.cart_item_quantity').innerText = --cartItem.quantity;
                cartItem.totalPrice = parseInt(cartItem.price) * cartItem.quantity;
                cartItemDOM.querySelector(".cart_item_price").innerText = cartItem.totalPrice + " Azn";

                localStorage.setItem('cart', JSON.stringify(cart));
                countCartTotal();
            } else {
                removeItem(product, cartItemDOM, addToCartButtonDOM);
            }

            if (cartItem.quantity === 1) {
                cartItemDOM.querySelector('[data-action="Decrease_Item"]').classList.add('btn-danger');
            }
        }
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
    countProductTotal();
    if (cart.length < 1) {
        document.querySelector('.cart-footer').remove();
    }
}

function addCartFooter() {
    if (document.querySelector('.cart-footer') === null) {
        cartDOM.insertAdjacentHTML('afterend', `
    <div class="cart-footer">
         <button class="btn btn-danger" data-action="Clear_Cart">Səbəti Təmizlə</button>
         <a class="btn btn-primary" href="/Product/Checkout" data-action="Checkout">Ödə</a>

    </div>
`);
        document.querySelector('[data-action="Clear_Cart"]').addEventListener('click', () => clearCart());
        document.querySelector('[data-action="Checkout"]').addEventListener('click', () => checkOut());
    }
} 
function clearCart() {
    cartDOM.querySelectorAll('.cart_item').forEach(cartItemDOM => {
        cartItemDOM.classList.add('cart_item_removed');
        setTimeout(() => cartItemDOM.remove(), 250);
    });

    cart = [];
    localStorage.removeItem('cart');
   
    countProductTotal();
    addToCartButtonsDOM.forEach(addToCartButtonDOM => {
        addToCartButtonDOM.innerText = 'Səbətə Əlavə Et';
        addToCartButtonDOM.disabled = false;

    });

}


function countCartTotal() {
    let cartTotal = 0;
    cart.forEach(cartItem => {
        cartTotal += parseInt(cartItem.quantity) * parseInt(cartItem.price);
    });
    document.querySelector("[data-action='Checkout']").innerText = "Ödə " + cartTotal + " Azn";
}
countProductTotal();
function countProductTotal() {
    $(".header-cart .items").empty();
    document.querySelector(".header-cart .items").innerText = cart.length;
    
}


/*********************************************/

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