$(document).ready(() => {

    $("#comments").click(function () {
        
        if (!($(".hide-show").css('display') == 'none')) {
            $(".hide-show").slideUp("slow");
        } else
        {
            $(".hide-show").slideDown("slow");
        }
    });

    $('#deleteBtn, .admin-delete-btn').click(() => {
        if (!confirm('Are you sure?'))
            return false;

    });

    var editorElement = document.querySelector('#editor');
    if (editorElement) {
        ClassicEditor
            .create(editorElement)
            .catch(error => {
                console.error(error);
            });
    }

    var stickyNavTop = $('.navSticky').offset().top;


    var stickyNav = function () {
        var scrollTop = $(window).scrollTop();


        if (scrollTop > stickyNavTop) {
            $('.navSticky').addClass('sticky-nav');
            $('body').css('margin-top', '60px');
        } else {
            $('.navSticky').removeClass('sticky-nav');
            $('body').css('margin-top', '0');
        }
    };

    stickyNav();

    $(window).scroll(function () {
        stickyNav();

    });

});