//Função para obter Token
function add_anti_forgery_token(data) {

    //obtem o valor do Token
    data.__RequestVerificationToken = $('[name=__RequestVerificationToken').val();
    return data;
}

function formata_msg_aviso(mensagens) {

    var ret = '';

    for (var i = 0; i < mensagens.length; i++) {
        ret += '<li>' + mensagens[i] + '</li>';
    }
    return '<ul>' + ret + '</ul>';
}

function abrir_form(dados) {
    //esconder mensagens
    $('#msg_mensagem_aviso').empty();
    $('#msg_aviso').hide();
    $('#msg_mensagem_aviso').hide();
    $('#msg_erro').hide();

    set_dados_forms(dados);

    //objeto Modal_cadastro
    var modal_cadastro = $("#modal_cadastro");

    //criar janela dialogo
    bootbox.dialog({
        title:'Cadastro de ' + tituloPag,
        //Chamar Janela de Cadastro
        message: modal_cadastro
    })
        //quando mostrar a janela (modal boostrap)
        .on('shown.bs.modal', function () {
            modal_cadastro.show(0, function () {
                set_focus_forms();
            })
        })
        //quando esconder a janela
        .on('hidden.bs.modal', function () {
            //adicione o objeto modal_cadastro invisivel ao corpo
            modal_cadastro.hide().appendTo('body')
        });
}
function criar_linha_grid(dados) {
    var ret =
        '<tr data-id=' + dados.Id + '>' +
        set_dados_grid(dados) +
        '<td>' +
        '<a class="btn btn-primary btn_alterar" style=margin-right:3px><i class="glyphicon glyphicon-pencil"></i> Alterar</a>' +
        '<a class="btn btn-danger btn_excluir"><i class="glyphicon glyphicon-trash"></i> Excluir</a>' +
        '</td>' +
        '</tr>';

    return ret;
}
//Clique do btn_incluir
$(document).on('click', '#btn_incluir', function () {
    //chamar a função e preencher todos como vazio
    abrir_form(get_dados_inclusao());
})
//clique dos botões btn_alterar
$(document).on('click', '.btn_alterar', function () {
    //obter botão
    var btn = $(this),
        //Obter o id da tag 'tr' mais proxima
        id = btn.closest('tr').attr('data-id'),
        //ação do controler cadastro, funcao = RecuperarGrupoProduto. (ação, controler)
        url = url_alterar,
        //param - parametro da funcao RecuperarGrupoProduto
        param = { 'id': id };
    //executar a ação em metodo post , response = objeto retornado da função
    $.post(url, add_anti_forgery_token(param), function (response) {
        if (response) {
            abrir_form(response);
        }
    });
})
//clique do botao excluir
$(document).on('click', '.btn_excluir', function () {
    var btn = $(this),
        //obter tr mais proxima
        tr = btn.closest('tr'),
        //Obter o id da tag 'tr'
        id = tr.attr('data-id'),
        //ação do controler cadastro, funcao = ExcluirGrupoProduto. (ação, controler)
        url = url_excluir,
        //param - parametro da funcao RecuperarGrupoProduto
        param = { 'id': id };

    //abrir janela confirmando a exclusão
    bootbox.confirm({
        message: "Realmente deseja excluir o " + tituloPag + "?",
        buttons: {
            confirm: {
                label: "Sim",
                className: "btn-danger"
            },
            cancel: {
                label: "Não",
                className: "btn-success"
            }
        },
        //função ao excluir
        callback: function (result) {
            if (result) {

                //executar a ação em metodo post , response = retorno da ação (sim ou não)
                $.post(url, add_anti_forgery_token(param), function (response) {
                    if (response) {
                        tr.remove();
                    }
                });
            }
        }
    })
})
    .on('click', '#btn_confirmar', function () {
        var btn = $(this),
            url = url_confirmar,

            //parametros em formato json que vai transformar no objeto GrupoProdutoModel
            param = get_dados_forms();

        $.post(url, add_anti_forgery_token(param), function (response) {
            //retorno do backend
            if (response.Resultado == "OK") {
                //Incluir linha na tabela
                if (param.Id == 0) {
                    param.Id = response.IdSalvo;
                    //obter tabela
                    var table = $('#grid_cadastro').find('tbody'),
                        //criar linha com o objeto retornado
                        linha = criar_linha_grid(param);

                    table.append(linha);

                }
                //se estiver alterando atualize as informações
                else {
                    //busco a linha tr no grid cadastro de acordo com o id, e depois busco o td dessa linha
                    var linha = $('#grid_cadastro').find('tr[data-id=' + param.Id + ']').find('td');
                    preencher_linha_grid(linha, param);
                }
                $('#modal_cadastro').parents('.bootbox').modal('hide');
            }
            if (response.Resultado == "ERRO") {
                $('#msg_aviso').hide();
                $('#msg_mensagem_aviso').hide();
                //mostrar erro
                $('#msg_erro').show();
            }

            if (response.Resultado == "AVISO") {

                //adcionar mensagem de erro (do backend) ao HTML
                $('#msg_mensagem_aviso').html(formata_msg_aviso(response.Mensagem));
                //Mostrar aviso
                $('#msg_aviso').show();
                //mostrar mensagem
                $('#msg_mensagem_aviso').show();
                //esconder erro
                $('#msg_erro').hide();
            }
        });
    })
    //click na paginação
    .on('click', '.page-item', function () {
        var btn = $(this),
            TamPag = $('#ddl_tam_pag').val(),
            pagina = btn.text(),
            url = url_page_click,
            param = { 'pagina': pagina, 'tamPag': TamPag };
        $.post(url, add_anti_forgery_token(param), function (response) {
            if (response) {
                var table = $('#grid_cadastro').find('tbody');

                table.empty();

                for (var i = 0; i < response.length; i++) {
                    table.append(criar_linha_grid(response[i]));
                }
                //remover o atributo active de todos botoes de paginação
                btn.siblings().removeClass('active');

                //adicionar ao botão clicado
                btn.addClass('active');
            }
        });
    })
    .on('change', '#ddl_tam_pag', function () {
        var ddl = $(this),
            TamPag = ddl.val(),
            pagina = 1,
            url = url_tam_pag_change,
            param = { 'pagina': pagina, 'tamPag': TamPag };
        $.post(url, add_anti_forgery_token(param), function (response) {
            if (response) {
                var table = $('#grid_cadastro').find('tbody');

                table.empty();

                for (var i = 0; i < response.length; i++) {
                    table.append(criar_linha_grid(response[i]));
                }
                //remover o atributo active de todos botoes de paginação
                ddl.siblings().removeClass('active');

                //adicionar ao botão clicado
                ddl.addClass('active');
            }
        });
    });