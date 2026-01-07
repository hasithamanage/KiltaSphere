# Stage 1: Use Node 20 as the base for the development environment
FROM node:20-alpine AS development

# Set the working directory inside the container
WORKDIR /app

# Expose the port that the Vite dev server uses
EXPOSE 5173

# The command that runs when the container starts
CMD ["npm", "run", "dev", "--", "--host"]