$(document).ready(function () {
    $('[data-toggle="tooltip"]').tooltip()
    $('body').on('click', '.btnAddToCart', function (e) {
        e.preventDefault()
        const id = parseInt($(this).data('id'))
        const quantity = parseInt($('#quantity_value').text()) || 1

        $.ajax({
            url: '/shoppingCart/addToCart',
            method: 'POST',
            data: { id, quantity },
            success: function (res) {
                $('#checkout_items').text(res.count)
            }

        })
    })

    $.ajax({
        url: '/shoppingCart/getCountInCart',
        method: 'GET',
        success: function (res) {
            $('#checkout_items').text(res.count)
        }
    })


})