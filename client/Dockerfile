# pull official base image
FROM node:14-alpine as build

# set working directory
WORKDIR /app

# add `/app/node_modules/.bin` to $PATH
ENV PATH /app/node_modules/.bin:$PATH

# install app dependencies
COPY package.json ./
COPY package-lock.json ./
RUN npm ci --silent
RUN npm install react-scripts@3.4.1 -g --silent

# add app
COPY . ./

RUN npm run-script build

FROM nginx:1.19-alpine AS nginx
COPY ./nginx/nginx.conf /etc/nginx/conf.d/default.conf
COPY --from=build ./app/build /usr/share/nginx/html