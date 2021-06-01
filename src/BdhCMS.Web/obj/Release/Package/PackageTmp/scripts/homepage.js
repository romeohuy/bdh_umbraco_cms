var new_product_slide;
$(document).ready(function() {
    $('#home-slider .owl-carousel').owlCarousel({
        items: 1,
        loop: true,
        autoplay:true,
        lazyLoad:true,
        lazyLoadEager:2,
        autoplayHoverPause: true
    });
});

$('.slide-next').click(function() {
    new_product_slide.trigger('next.owl.carousel');
});

$('.slide-pre').click(function() {
    new_product_slide.trigger('next.owl.carousel');
});