$('#btn-back-to-top').click(function() {
    $('html, body').animate({
        scrollTop: 0
    }, 700);
    return false;
});

$(window).scroll(function() {
    var scrollTopVal = $(this).scrollTop();
    if (scrollTopVal < 200) {
        $('#btn-back-to-top').fadeOut();
    } else {
        $('#btn-back-to-top').fadeIn();
    }
});
$('.scroll-animate').click(function() {
    $('body,html').animate({
        scrollTop: $($(this).attr('href')).offset().top - 70
    }, 800);
});