// Sayfa yüklendiğinde
//$(function () {
$(document).ready(function () {
    $(".modal-action")
        .on('show.bs.modal', function (e) {
            let button = $(e.relatedTarget);
            let url = button.attr("href");
            let modal = $(this);
            modal.find('.modal-content').html('Loading...');
            modal.find('.modal-content').load(url);
        })
        .on('hidden.bs.modal', function (e) {
            var modal = $(this);
            modal.removeData('bs.modal');
            modal.find('.modal-content').empty();
        });

    // modal-action içerisindeki form elemanını submit yaptığımızda
    /*
     * Ajax ile yüklenen içeriklerde bind yapısı aşağıdaki gibi yapmak yerine document üzerinden yapılır.
     **/
    /*
    $('.modal.action form')
        .on('submit', function () {
            alert(1);
        });

    document.querySelector(".modal-action form")
        .addEventListener("submit", function () {
            alert(2);
        });
        */

    $(document).on("submit", '.modal-action form', function (event) {
        // form'un otomatik gönderilmesini engelle
        event.preventDefault();

        // form'u kendimiz ajax post ile göndereceğiz
        let form = $(this);
        let url = form.attr("action");
        let modal = $(".modal-action");
        let formData = form.serialize();

        modal.find('.modal-content').html('Processing...');

        // AJAX post işlemi yap, yani formu gönderme işlemini
        $.post(url, formData)
            .done(function (result) {
                if (!result.isSuccess) {
                    toastr.error("An error occured!", "Post Error");
                    modal.find('.modal-content').html(result);
                } else {
                    location.href = result.redirect;
                }
            })
            .fail(function (e) {
                toastr.error("An error occured", "Fail");
            });
    });
});