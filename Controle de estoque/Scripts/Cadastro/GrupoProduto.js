﻿
function set_dados_forms(dados) {
    //preencher o campo id_cadastro
    $("#id_cadastro").val(dados.Id);

    //preencher textbox e checkbox
    $('#txt_nome').val(dados.Nome);
    $('#cbx_ativo').prop('checked', dados.Ativo);
}
function set_focus_forms() {
    //textebos ja selecionado
    $("#txt_nome").focus();
}
function set_dados_grid(dados) {
    return '<td>' + dados.Nome + '</td>' +
        '<td>' + (dados.Ativo ? "SIM" : "NÃO") + '</td>';
}

function get_dados_inclusao() {
    return { Id: 0, Nome: "", Ativo: true };
}
function get_dados_forms() {
    return {
        Id: $("#id_cadastro").val(),
        Nome: $("#txt_nome").val(),
        Ativo: $("#cbx_ativo").prop('checked')

    };
}
function preencher_linha_grid(linha, param) {
    linha
        //pego a coluna 0 e adiciono o nome (metodo 'end' permite chamar duas funções de linha)
        .eq(0).html(param.Nome).end()
        //pego a coluna 1 e preencho com o ativo
        .eq(1).html(param.Ativo ? 'SIM' : 'NÃO');
}
