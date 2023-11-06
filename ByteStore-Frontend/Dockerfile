# Use the official Node.js runtime as a parent image
FROM node:latest AS build

# Set the working directory in the container
WORKDIR /usr/local/app

COPY . .
# COPY ./ /usr/local/app/


# Install project dependencies
RUN npm install

# Build the Angular app for production
RUN npm run build

FROM nginx:alpine
COPY --from=build /usr/local/app/dist/ecommerce-front/ /usr/share/nginx/html
COPY ./config/nginx.conf /etc/nginx/conf.d/default.conf

# Expose the port the app runs on
EXPOSE 80

# Define the command to run your Angular app
# CMD ["ng","serve","--host", "0.0.0.0"]


# docker build -t bytestore-frontend-image .
# docker run -p 4200:80 bytestore-frontend-image