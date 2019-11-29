/*****Toggel****/

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
    coll[i].addEventListener("click", function () {
        this.classList.toggle("active");
        var content = this.nextElementSibling;
        if (content.style.display === "block") {
            content.style.display = "none";
        } else {
            content.style.display = "block";
        }
    });
}

$(document).ready(function () {
    $('.categ-slider').slick({
       
            slidesToShow: 3,
            slidesToScroll: 1,
            autoplay: true,
        autoplaySpeed: 2000,
        responsive: [{

            breakpoint: 1024,
            settings: {
                slidesToShow: 3,
                infinite: true
            }

        }, {

                breakpoint: 998,
                settings: {
                    slidesToShow: 2,
                    dots: true
                }

            },{

            breakpoint: 600,
            settings: {
                slidesToShow: 1,
                dots: true
            }

        }, {

            breakpoint: 300,
            settings: "unslick" // destroys slick

        }]
 
  });
});




$(document).ready(function () {
    $('.all-categ').tilt({
        maxTilt: 10,
        glare: false,
        maxGlare: .4,
    })
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






$(window).on("load", function () {
    $(".loadingAnima").hide();
});

var acc = document.getElementsByClassName("accordion");

for (var j = 0; j < acc.length; j++) {
    acc[j].addEventListener("click", function () {
        this.classList.toggle("active");
        var panel = this.nextElementSibling;
        if (panel.style.display === "block") {
            panel.style.display = "none";
        } else {
            panel.style.display = "block";
        }
    });
}


