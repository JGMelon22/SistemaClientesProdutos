// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
let botaoListarClientes = document.getElementById("ListarClientesBotao");
let botaoListarProdutos = document.getElementById("ListarProdutosBotao");
let spinnerLoading = document.getElementById("LoadingSpinner");

botaoListarClientes.onclick = function () {
    spinnerLoading.style.display = "block";
}

botaoListarProdutos.onclick = function () {
    spinnerLoading.style.display = "block"
}