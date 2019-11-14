'use strict';



// $(".btnParent").on("click", function(){
//     $(this).next(".target").slideUp()
//      $(this).next(".target").slideDown(300);   
//   })
  $(function () {
    let cart = [];
    const cartDOM = document.querySelector(".PcText");
      const addToCartButtonsDOM = document.querySelectorAll('[data-action="AddToCart"]');
      if (cart.length > 0) {
          cart.forEach(cartItem => {
              const product = cartItem;
              insertItemToDOM(product);
              //countCartTotal();

              addToCartButtonsDOM.forEach(addToCartButtonDOM => {
                  const productDOM = addToCartButtonDOM.parentNode.parentNode;


                  if (productDOM.querySelector('.pro_name').innerText.toUpperCase().trim() === product.name.toUpperCase().trim()) {

                      handleActionButtons(addToCartButtonDOM, product);
                  }
              });

          });
      }

      addToCartButtonsDOM.forEach(addToCartButtonDOM => {
          addToCartButtonDOM.addEventListener('click', () => {
              const productDOM = addToCartButtonDOM.parentNode.parentNode;
              const product = {
                  pId: productDOM.querySelector(".btnPro").getAttribute('pId'),
                  image: productDOM.querySelector('.pro_image').getAttribute('src'),
                  name: productDOM.querySelector('.pro_name').innerText,
                  price: productDOM.querySelector('.pro_price').innerText,
                  quantity: 1,
                  totalPrice: 0

              };
              const isInCart = (cart.filter(cartItem => (cartItem.name === product.name)).length > 0);

              if (!isInCart) {
                  insertItemToDOM(product);
                  cart.push(product);
                  countCartTotal();
                  handleActionButtons(addToCartButtonDOM, product);
              }
          });
      });

      

      function insertItemToDOM(product) {
          cartDOM.insertAdjacentHTML('beforeend', `
    <div class="cart_item">

      <a class="cart_item_name text-dark" href="#">${product.name}</a>
      <h5 class="cart_item_price">${(product.totalPrice == 0 ? product.price : product.totalPrice)}</h5>
        <div class="quantity_items">
              <button class="btn btn-primary btn-sm ${(product.quantity === 1 ? ' btn-danger' : '')}" data-action="DecreaseItem">&minus;</button>
              <h5 class="cart_item_quantity">${product.quantity}</h5>
              <button class="btn btn-primary btn-sm" data-action="IncreaseItem">&plus;</button>
        </div>
          <button class="btn btn-danger btn-sm" data-action="RemoveItem">&times;</button>

    </div>
  `);
          addCartFooter();
      }

      function handleActionButtons(addToCartButtonDOM, product) {
          addToCartButtonDOM.innerText = 'Əlavə Edildi';
          addToCartButtonDOM.disabled = true;

          const cartItemsDOM = cartDOM.querySelectorAll('.cart_item');
          console.log(cartItemsDOM)
          cartItemsDOM.forEach(cartItemDOM => {
              console.log(cartItemDOM.querySelector('.cart_item_name').innerText)
              if (cartItemDOM.querySelector('.cart_item_name').innerText.toUpperCase().trim() === product.name.toUpperCase().trim()) {
                  cartItemDOM.querySelector('[data-action="IncreaseItem"]').addEventListener('click', () => increaseItem(product, cartItemDOM));
                  cartItemDOM.querySelector('[data-action="DecreaseItem"]').addEventListener('click', () => decreaseItem(product, cartItemDOM, addToCartButtonDOM));
                  cartItemDOM.querySelector('[data-action="RemoveItem"]').addEventListener('click', () => removeItem(product, cartItemDOM, addToCartButtonDOM));
              }
          });
      }

      

      function increaseItem(product, cartItemDOM) {
          cart.forEach(cartItem => {
              if (cartItem.name === product.name) {
                  cartItemDOM.querySelector('.cart_item_quantity').innerText = ++cartItem.quantity;
                  cartItemDOM.querySelector('[data-action="DecreaseItem"]').classList.remove('btn-danger');
                  countCartTotal();
                  cartItem.totalPrice = parseInt(cartItem.price) * cartItem.quantity;
                  cartItemDOM.querySelector(".cart_item_price").innerText = cartItem.totalPrice;

              }
          });
      }

      function decreaseItem(product, cartItemDOM, addToCartButtonDOM) {
          cart.forEach(cartItem => {
              if (cartItem.name === product.name) {
                  if (cartItem.quantity > 1) {
                      cartItemDOM.querySelector('.cart_item_quantity').innerText = --cartItem.quantity;
                      countCartTotal();
                  } else {
                      removeItem(product, cartItemDOM, addToCartButtonDOM);
                  }

                  if (cartItem.quantity === 1) {
                      cartItemDOM.querySelector('[data-action="DecreaseItem"]').classList.add('btn-danger');
                  }
              }
          });
      }

      function removeItem(product, cartItemDOM, addToCartButtonDOM) {
          console.log("removed")
          cartItemDOM.classList.add('cart_item_removed');
          setTimeout(() => cartItemDOM.remove(), 250);
          cart = cart.filter(cartItem => cartItem.name !== product.name);
          countCartTotal();
          addToCartButtonDOM.innerText = 'Əlavə Et';
          addToCartButtonDOM.disabled = false;

          if (cart.length < 1) {
              document.querySelector('.cart-footer').remove();
          }
      }

      function addCartFooter() {
          if (document.querySelector('.pccart-footer') === null) {
              cartDOM.insertAdjacentHTML('afterend', `
               
            <div class="pccart-footer">
                    <button class="btn btn-danger" data-action="clearpc_Cart">Səbəti Təmizlə</button>
                    <button style="color:#fff !important" class="btn btn-primary" data-action="checkoutpc"><a class="text-white" href="/Product/CheckOut">Səbətə Əlavə Et</a></button>
            </div>
`);
              document.querySelector('[data-action="clearpc_Cart"]').addEventListener('click', () => clearCartpc());
              document.querySelector('[data-action="checkoutpc"]').addEventListener('click', () => checkOutpc() );
          }
      }
      function clearCartpc() {
          cartDOM.querySelectorAll('.cart_item').forEach(cartItemDOM => {
              cartItemDOM.classList.add('cart_item_removed');
              setTimeout(() => cartItemDOM.remove(), 250);
          });
          cart = [];
          addToCartButtonsDOM.forEach(addToCartButtonDOM => {
              addToCartButtonDOM.innerText = 'Əlavə Et';
              addToCartButtonDOM.disabled = false;

          });

      }

      function checkOutpc() {
          localStorage.setItem('cart', JSON.stringify(cart));

      }

      function countCartTotal() {
          let cartTotal = 0;
          cart.forEach(cartItem => {
              cartTotal = cartTotal + (cartItem.quantity * cartItem.price);
          });

      }

});



$(function() {
  $('.btnParent').click(function(){
    var incomeDiv = $(this).next('div');
    $(incomeDiv).toggle();        
    $('div[id^=target]').not(incomeDiv).hide();
 });
});





  $(".btnPro").on("click",function(){
    $(this).parents(".group").find("div[data-foldable-role='target']").slideUp(300);
      $(this).parents(".group").next().find("div[data-foldable-role='target']").slideDown(300);
  })