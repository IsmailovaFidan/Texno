/*****Toggel****/
$(".level-1").click(function(){
    $(".drop-menu.drop-active").removeClass("drop-active")
   $(this).find(".drop-menu").toggleClass("drop-active")
    
});
$(function () {
    $(".leftBox li").on('click', function () {
        $(".leftBox li").removeClass("chActive")
        $(this).addClass("chActive")
        let tab_id = $(this).attr("data-ng-class")

        $('.form-set').removeClass("d-none");
        $('.form-set').hide();
        $("#" + tab_id).show();

    })
    
})




function toggleIcon(e) {
    $(e.target)
        .prev('.panel-heading')
        .find(".more-less")
        .toggleClass('glyphicon-plus glyphicon-minus');
}
$('.panel-group').on('hidden.bs.collapse', toggleIcon);
$('.panel-group').on('shown.bs.collapse', toggleIcon);
	
var coll = document.getElementsByClassName("collapsible");
var i;

for (i = 0; i < coll.length; i++) {
    coll[i].addEventListener("click", function() {
        this.classList.toggle("active");
        var content = this.nextElementSibling;
        if (content.style.display === "block") {
            content.style.display = "none";
        } else {
            content.style.display = "block";
        }
    });
}
/****/
$(window).scroll(function(){
    if ($(window).scrollTop() >= 300) {
        $('.main-nav').addClass('fixed-header');
        $('nav div').addClass('visible-title');
    }
    else {
        $('.main-nav').removeClass('fixed-header');
        $('nav div').removeClass('visible-title');
    }
});
  

$(document).ready(function () {
    var slideCount = $('#slider2 ul li').length;
    var slideWidth = $('#slider2 ul li').width();
    var slideHeight = $('#slider2 ul li').height();
    var sliderUlWidth = slideCount * slideWidth;

    $('#slider2').css({
        width: slideWidth,
        height: slideHeight
    });
    $('#slider2 ul').css({
        width: sliderUlWidth,
        marginLeft: -slideWidth
    });
    $('#slider2 ul li:last-child').prependTo('#slider2 ul');

    function moveLeft() {
        $('#slider2 ul').animate({
            left: +slideWidth
        }, 200, function () {
            $('#slider2 ul li:last-child').prependTo('#slider2 ul');
            $('#slider2 ul').css('left', '');
        });
    };

    function moveRight() {
        $('#slider2 ul').animate({
            left: -slideWidth
        }, 200, function () {
            $('#slider2 ul li:first-child').appendTo('#slider2 ul');
            $('#slider2 ul').css('left', '');
        });
    };

    // Slider Arrows
    $('#slider2 .prevarrow2').click(function () {
        moveLeft();
    });
    $('#slider2 .nextarrow2').click(function () {
        moveRight();
    });

    // auto play function
    setInterval(function () {
        moveRight();
    }, 6000);
    $(".counter").counterUp({

        delay: 50,
        time: 4000
    });
    $('.all-categ').tilt({
        maxTilt: 10,
        glare: false,
        maxGlare: .4
    });
    var search_icon = $("#search-icon-0e7350d");
    search_icon.on('click', function () {
        $('#search-overlay-0e7350d').addClass('open');
    });

    $('.search-close').on('click', function () {
        $('#search-overlay-0e7350d').removeClass('open');
    });

    $('.play').on('click', function () {
        owl.trigger('play.owl.autoplay', [1000])
    });
    $('.stop').on('click', function () {
        owl.trigger('stop.owl.autoplay');
    });
    $('.owl-carousel').owlCarousel({
        loop: true,
        autoplay: true,
        margin: 10,
        nav: true,
        items: 9,
        autoplaySpeed: 500,
        autoplayHoverPause: true,
        responsive: {
            0: {
                items: 2
            },
            600: {
                items: 3
            },
            1000: {
                items: 5,
                loop: true,
                autoplay: true

            }
        }
    });


});




$(".two-menu").click(function(){
    $(".menu-vertical").toggleClass("open");
    
});

$(document).ready(mySlider);

function mySlider() {
    var imgNumber, 
        sliderContainerWidth, 
        imgContainer,
        index,
        flag = true,
        speed = 600,
        bullets = true,
        auto = true ,
        time = 5000 
    construction();
    $(window).resize(construction);
    if(auto){
        var handle = setInterval(slideRight, time); 
    }


    function construction() {
        index = 1;
        imgNumber = $('.images-container li').length;
        sliderContainerWidth = Math.round($('#slider-container').width());
        imgContainer = sliderContainerWidth * imgNumber;
        $('.images-container').css("width", imgContainer);
        $('.images-container li').css("width", sliderContainerWidth);
        $('.images-container').css("margin-left", 0);
        if (bullets === true) {
            $('.bullets-container').html("");
            for (i = 1; i <= imgNumber; i++) {
                $('.bullets-container').append("<span class='bullet'></span>");
            }
            $('.bullet').eq(0).addClass('active-slide');
        }
        $(".bullet").click(pagers);
        $('.a-right').click(slideRight);
        $('.a-left').click(slideLeft);
    }

    function pagers() {
        if (!$(this).hasClass('active-slide')) {
            var bulletIndex = $(".bullets-container span").index(this) + 1;
            index = bulletIndex;
            $(".bullets-container").find(".bullet").removeClass("active-slide").eq(bulletIndex - 1).addClass("active-slide");
            $('.images-container').animate({
                marginLeft: -sliderContainerWidth * (bulletIndex - 1)
            }, speed);
        }
    }

    function slideRight() {
        var imgContainerLeft = parseInt($('.images-container').css('margin-left'));
        if (flag) {
            if (imgContainerLeft === -sliderContainerWidth * (imgNumber - 1)) {
                index = 1;
                $('.images-container').animate({
                    marginLeft: 0
                }, speed, function () {
                    flag = true;
                });
            } else {
                index++;
                $('.images-container').animate({
                    marginLeft: '-=' + sliderContainerWidth
                }, speed, function () {
                    flag = true;
                });
            }
            flag = false;
            $(".bullets-container").find(".bullet").removeClass("actactive-slideive").eq(index - 1).addClass("active-slide");
        }
    }

    function slideLeft() {
        var imgContainerLeft = parseInt($('.images-container').css('margin-left'));
        clearInterval(slideRight, 3000);
        if (flag) {
            if (imgContainerLeft === 0 ) {
                index = index + (imgNumber - 1);
                $('.images-container').animate({
                    marginLeft: -sliderContainerWidth * (imgNumber - 1) + 'px'
                }, speed, function() {
                    flag = true;
                })
            } else {
                index--;
                $('.images-container').animate({
                    marginLeft: '+=' + sliderContainerWidth
                }, speed, function () {
                    flag = true;
                });
            }
            flag = false;
            $(".bullets-container").find(".bullet").removeClass("active-slide").eq(index - 1).addClass("active-slide");
        }

    }
    $("#slider-container .arrow , .bullets-container").hover(function () {
        clearInterval(handle);
    }, function () {
        handle = setInterval(slideRight, time);
    });
  
}
$(window).on("load", function () {
    $(".loadingAnima").hide();
});

var acc = document.getElementsByClassName("accordion");

for (var j = 0; j < acc.length; j++) {
  acc[j].addEventListener("click", function() {
    this.classList.toggle("active");
    var panel = this.nextElementSibling;
    if (panel.style.display === "block") {
      panel.style.display = "none";
    } else {
      panel.style.display = "block";
    }
  });
}


