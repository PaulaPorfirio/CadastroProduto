<template>
    <div class="container">

        <h1>AppCadastro – </h1>
        <h1 class="TituloCadastro">Produtos</h1>
            <div class="toolbar">
                <button type="button" class="btn" @click="carregar" :disabled="carregando">
                    {{ carregando ? 'Listando...' : 'Listar' }}
                </button>
                <button type="button" class="btn btn-Novo" @click="novo">Novo</button>
            </div>

            <form class="card" @submit.prevent="salvar">
                <h2>{{ editId ? 'Edição' : 'Novo Produto' }}</h2>
                <h2 class="EdicaoProdutoNome" v-if="editId">{{ '- ' + form.nome }}</h2>
                <label>Nome: <input v-model="form.nome" required /></label>
                <label>Categoria: <input v-model="form.categoria" required /></label>
                <label>Preço: <input v-model.number="form.valorUnitario" required type="number" min="0" step="0.01" /></label>
                <label>Quantidade: <input v-model.number="form.quantidade" required type="number" min="0" /></label>

                <div class="acoes">
                    <button type="submit" class="btn btn-primary">
                        {{ editId ? 'Salvar alterações' : 'Salvar' }}
                    </button>
                    <button type="button" v-if="editId" class="btn btn-Novo" @click="novo">Cancelar</button>
                </div>
            </form>

            <div v-if="carregando">Carregando...</div>
            <div class="erro" v-if="erro">{{ erro }}</div>

            <table v-if="!carregando && itens.length" class="tabela">
                <thead>
                    <tr>
                        <th>Nome</th>
                        <th>Categoria</th>
                        <th style="text-align:right;">Preço</th>
                        <th style="text-align:right;">Qtd</th>
                        <th style="text-align:center;">Disp.</th>
                        <th style="width:180px; text-align:center;">Ações</th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="p in itens" :key="p.codigo">
                        <td>{{ p.nome }}</td>
                        <td>{{ p.categoria }}</td>
                        <td style="text-align:right;">{{ moeda(p.valorUnitario) }}</td>
                        <td style="text-align:right;">{{ p.quantidade }}</td>
                        <td style="text-align:center;">{{ p.quantidade > 0 ? 'Sim' : 'Não' }}</td>
                        <td style="text-align:right;">
                            <button type="button" class="btn btn-acoes" @click="editarUI(p)">Editar</button>
                            <button type="button" class="btn btn-Deletar btn-acoes" @click="excluirUI(p.codigo)">X</button>
                        </td>
                    </tr>
                </tbody>
            </table>
    </div>
</template>

<script setup>
    import { ref, onMounted } from 'vue'
    import { listar, criar, editar, excluir } from './api'

    const itens = ref([])
    const carregando = ref(false)
    const erro = ref('')

    const vazio = { nome: '', categoria: '', valorUnitario: 0, quantidade: 0 }
    const form = ref({ ...vazio })
    const editId = ref(null)

    const moeda = v => Number(v ?? 0).toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' })

    async function carregar() {
        try {
            carregando.value = true; erro.value = ''
            itens.value = await listar()
        } catch (e) { console.error(e); erro.value = 'Falha ao carregar.' }
        finally { carregando.value = false }
    }

    function editarUI(p) {
        editId.value = p.codigo
        form.value = { nome: p.nome, categoria: p.categoria, valorUnitario: p.valorUnitario, quantidade: p.quantidade }
        window.scrollTo({ top: 0, behavior: 'smooth' })
    }

    function novo() {
        editId.value = null
        form.value = { ...vazio }
    }

    async function salvar() {

        if (!form.value.nome?.trim() || !form.value.categoria?.trim()) return alert('Preencha Nome e Categoria.')
        if (Number(form.value.valorUnitario) < 0 || Number(form.value.quantidade) < 0) return alert('Preço/Quantidade devem ser ≥ 0.')

        try {
            if (editId.value) {
                await editar(editId.value, { ...form.value, codigo: editId.value })
            } else {
                await criar(form.value)
            }
            novo()
            await carregar()
        } catch (e) {
            console.error('Salvar ERRO:', e?.response?.status, e?.response?.data || e)
            alert('Erro ao salvar.')
        }
    }

    async function excluirUI(id) {
        if (!id) return
        if (!confirm('Confirma a exclusão deste produto?')) return
        try {
            await excluir(id)
            await carregar()
        } catch (e) {
            console.error(e); alert('Erro ao excluir.')
        }
    }

</script>

<style>
    :root {
        font-family: system-ui, sans-serif;
        background: linear-gradient(90deg, #f0f6ff, #cedbed, #68a1f7, #568bdb);
    }

    .container {
        max-width: 900px;
        margin: 24px auto;
        padding: 16px;
    }

    h1 {
        color: #4370a3;
        font-style: italic;
    }

    .TituloCadastro {
        margin-top: -65px;
        margin-left: 228px;
        font-style: normal;
        color: #1053a1;
    }

    .EdicaoProdutoNome {
        margin-top: -50px;
        margin-left: 80px;
    }

    h2 {
        color: #1053a1;
        margin-top: 15px;
        margin-bottom: 9px;
    }

    .toolbar {
        display: flex;
        gap: 8px;
        margin-bottom: 12px;
    }

    .card {
        display: grid;
        gap: 8px;
        padding: 16px;
        border: 1px solid #cfe2ff;
        border-radius: 8px;
        margin-bottom: 16px;
        background: #fff;
        box-shadow: 0 2px 6px rgba(0,0,0,0.05);
    }

    label {
        display: grid;
        gap: 4px;
        color: #1a4e8a;
    }

    input {
        padding: 8px;
        border: 1px solid #99c2ff;
        border-radius: 4px;
    }

    .acoes {
        display: flex;
        gap: 8px;
        margin-top: 8px;
    }

    .btn {
        padding: 8px 23px;
        border: none;
        cursor: pointer;
        background: #4370a3;
        color: #fff;
        transition: background .2s;
        border-radius: 15px;
        font-size: 15px;
    }

        .btn:hover {
            background: #1669c1;
        }

        .btn:disabled {
            opacity: .7;
            cursor: not-allowed;
        }

    .btn-Novo {
        background: #5995d9;
        border-radius: 15px;
    }

        .btn-Novo:hover {
            background: #5a97f0;
        }

    .btn-Deletar {
        background: #d9534f;
    }

        .btn-Deletar:hover {
            background: #c44541;
        }

    .btn-acoes {
        padding: 6px 10px;
        font-size: .9rem;
        border-radius: 15px;
        margin-left: 28px;
    }

    .tabela {
        width: 100%;
        border-collapse: collapse;
        background: #fff;
        border-radius: 8px;
        overflow: hidden;
        box-shadow: 0 2px 6px rgba(0,0,0,0.05);
    }

        .tabela th {
            background: #e6f0ff;
            color: #1a4e8a;
        }

        .tabela th, .tabela td {
            border-top: 1px solid #eee;
            padding: 8px;
        }

    .erro {
        color: #b00020;
        margin: 8px 0;
    }
</style>
