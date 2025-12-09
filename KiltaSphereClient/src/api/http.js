// File: src/api/http.js
import axios from 'axios';

// IMPORTANT: Uses the confirmed HTTPS port for your running .NET API
const instance = axios.create({
  baseURL: 'https://localhost:7282/api', 
  timeout: 5000, 
  headers: {
    'Content-Type': 'application/json',
  },
});

export default instance;