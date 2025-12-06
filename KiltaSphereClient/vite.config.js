import { fileURLToPath, URL } from 'node:url'

import { defineConfig } from 'vite'
import vue from '@vitejs/plugin-vue'
import vueDevTools from 'vite-plugin-vue-devtools'

// https://vite.dev/config/
export default defineConfig({
  plugins: [
    vue(),
    vueDevTools(),
  ],
  resolve: {
    alias: {
      '@': fileURLToPath(new URL('./src', import.meta.url))
    },
  },

  // Configure the server host
  server: {
    host: '0.0.0.0', // This makes Vite listen on all available network interfaces
    port: 5173,      // Ensure the port matches Docker mapping
    watch: {
        usePolling: true // This can sometimes help with file changes on network drives (like X:)
    }
  }

})
