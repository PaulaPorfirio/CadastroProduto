import axios from 'axios'

const http = axios.create({
    baseURL: '/api/Produtos'
})

export async function listar() {
    const { data } = await http.get('/GetProdutos')
    return data
}

export async function criar(produto) {

    const { data } = await http.post('/AdicionaProduto', produto)
    return data
}

export async function editar(codigo, produto) {

    await http.put(`/AtualizaProduto`, produto, { params: { Codigo: codigo } })
    return true
}

export async function excluir(id) {
    await http.delete(`/RemoveProduto`, { params: { Id: id } })
    return true
}

export async function buscarPorCategoria(idCategoria) {
    const { data } = await http.get('/GetProdutoPorCategoria', { params: { Id: idCategoria } })
    return data
}
