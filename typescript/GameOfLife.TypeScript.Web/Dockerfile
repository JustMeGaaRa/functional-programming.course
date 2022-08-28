FROM node:lts-alpine
ENV NODE_ENV=production
WORKDIR /usr/src/app
COPY ["typescript/GameOfLife.TypeScript.Web/package.json", "typescript/GameOfLife.TypeScript.Web/package-lock.json*", "typescript/GameOfLife.TypeScript.Web/npm-shrinkwrap.json*", "./"]
RUN npm install --production --silent && mv node_modules ../
COPY . .
EXPOSE 3000
RUN chown -R node /usr/src/app/
WORKDIR /usr/src/app/typescript/GameOfLife.TypeScript.Web
USER node
CMD ["npm", "start"]
