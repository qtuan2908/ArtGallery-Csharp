$(document).ready(function () {
    var desiredHeight = 500; // Đặt chiều cao mong muốn của container carousel

    $(".owl-carousel").each(function (index, el) {
        // Đặt chiều cao của container carousel
        $(el).css('height', desiredHeight + 'px');

        $(el).find("img").each(function (index, img) {
            var w = $(img).prop('naturalWidth');
            var h = $(img).prop('naturalHeight');
            var newWidth = Math.round(desiredHeight * w / h);
            $(img).css({
                'width': newWidth + 'px',
                'height': desiredHeight + 'px',
                'object-fit': 'contain', // Đảm bảo rằng hình ảnh vừa vặn trong container
                'display': 'block', // Đảm bảo hình ảnh không bị inline
                'margin-left': 'auto', // Căn giữa hình ảnh theo chiều ngang
                'margin-right': 'auto' // Căn giữa hình ảnh theo chiều ngang
            });
        });

        $(el).owlCarousel({
            loop: true,
            margin: 20, // Đặt khoảng cách cố định giữa các ảnh
            nav: false,
            dots: false,
            center: true,
            autoplay: true,
            responsiveClass: true,
            autoplayTimeout: 4000,
            autoplaySpeed: 1500,
            responsive: {
                0: {
                    margin: 5,
                    items: 1, // Giảm số lượng ảnh để không bị chồng lên nhau trên màn hình nhỏ
                },
                739: {
                    margin: 10,
                    items: 1,
                },
                1024: {
                    items: 1,
                },
            }
        });
    });
});