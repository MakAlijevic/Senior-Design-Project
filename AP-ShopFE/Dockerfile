#Stage 1
FROM node:14-alpine as node
WORKDIR /app
COPY . .
RUN npm install -g npm@9.4.2
RUN npm run build

#Stage 2
FROM nginx:alpine
COPY --from=node /app/dist/ap-shop-fe /usr/share/nginx/html