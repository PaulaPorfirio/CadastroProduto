import { defineConfig } from 'vite'
import vue from '@vitejs/plugin-vue'

const API = 'https://localhost:7118'

export default defineConfig({
    plugins: [vue()],
    server: {
        proxy: {
            '/api': {
                target: API,
                changeOrigin: true,
                secure: false,
            },
        },
    },
})

