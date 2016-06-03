var url = window.location.origin;

function addSuccess(quantidade) {
    $(function () {
        $('.count-carrinho').html(quantidade);
        recalculaFrete(false);
    });
}

function removeCarrinhoSuccess(response) {
    recalculaFrete(false);
}

function finalizaPedido() {
    recalculaFrete(false);

    $.ajax({
        type: "POST",
        url: url + "/Carrinho/finalizarPedido"
    });
}

function recalculaFrete(only) {
        var cep = $("input[name=Endereco]:checked").val();
        var peso = $(".peso").html();
        var tipoFrete = $("input[name=tipoFrete]:checked").val();

        $.ajax({
            type: "POST",
            url: url + "/Carrinho/CalculaFrente",
            data: { cep: cep, peso: peso, tipoFrete: tipoFrete },
            success: function (response) {
                if (only) {
                    return true;
                } 

                window.location.replace(response.RedirectUrl + "?endereco=" + cep);
            }
        });
}

$(function () {
    $('.carrinho-quantidade').blur(function () {
        var id = $(this).closest(".list-group-item").attr("id");
        var quantidade = $(this).val();
        if (parseInt(quantidade) < 1) {
            $(this).val("1");
            return false;
        }
        $.ajax({
            type: "POST",
            url: url + "/Carrinho/Change",
            data: { id: id, quantidade: quantidade },
            success: function (response) {
                recalculaFrete(false);
                return true;
            }
        });
    });
    $('.calcular-frete').click(function () {
        recalculaFrete(false);
    });

    $("input[name=Endereco]").click(function () {
        recalculaFrete(false);
    });

    //comentado
    $("input[name=tipoFrete]").click(function () {
        recalculaFrete(false);
    });
});