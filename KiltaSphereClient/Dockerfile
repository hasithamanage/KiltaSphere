FROM node:20-alpine AS development

WORKDIR /app

# 1. Copy only the package files first (for efficiency)
COPY package*.json ./

# 2. Install dependencies inside the container
RUN npm install

# 3. Copy the rest of the code
COPY . .

# 4. Expose the port
EXPOSE 5173

# 5. Run the dev server with --host
CMD ["npm", "run", "dev", "--", "--host"]