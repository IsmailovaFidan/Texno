$(window).on("load", function () {
    $(".loadingAnima").hide();
});

// const accIcon=[...document.querySelectorAll(".accord-icon")];
// accIcon.forEach(function(icon){
//   icon.addEventListener("click",function(){
//       const PreAccIcon = document.querySelector(".panel.d-block");
//       PreAccIcon.classList.remove("d-block");

//       icon.parentElement().nextElementSibling().classList.add(".d-block")
//   });
    
// });
// $(function() {
//   $('.accord-icon').click(function(){
//     $(this).parent().parent().next().slideToggle();

//  });
 // });
$(".accord-icon").click(function(){
    
    $(".panel.panelactive").removeClass("panelactive");
    $(this).parent().parent().next().toggleClass("panelactive");
   
    
});






$(".two-menu").click(function(){
    $(".menu-vertical").toggleClass("open");
});

/****/
$(window).scroll(function(){
    if ($(window).scrollTop() >= 100) {
        $('.main-nav').addClass('fixed-header');
        $('nav div').addClass('visible-title');
    }
    else {
        $('.main-nav').removeClass('fixed-header');
        $('nav div').removeClass('visible-title');
    }
});

function filter() {
    document.getElementById("filter").style.width = "100%";
}
function list() {
    document.getElementById("list").style.width = "100%";
}
$(".finish").click(function () {
    closeFilter();
});
function closeFilter() {
    document.getElementById("filter").style.width = "0%";
    //document.getElementById("list").style.width = "0%";
}


