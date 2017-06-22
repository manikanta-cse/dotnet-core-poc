# Enabling TLS On NGINX

## Create a Self-Signed Cert

**NOTE: This is only for development purposes!**  Get an actual cert for non-dev usage.

```bash

# create directory to hold private keys
sudo mkdir /etc/ssl/private

# update permissions
sudo chmod 700 /etc/ssl/private

# create self-signed cert pair
sudo openssl req -x509 -nodes -days 365 -newkey rsa:2048 -keyout /etc/ssl/private/nginx-selfsigned.key -out /etc/ssl/certs/nginx-selfsigned.crt

# create a strong Diffie-Hellman group used in negotiating Perfect Forward Secrecy
sudo openssl dhparam -out /etc/ssl/certs/dhparam.pem 2048
```

## Configure NGINX To Use Certs

Create a new ssl.conf for NGINX: `sudo nano /etc/nginx/conf.d/ssl.conf`

Set the contents of ssl.conf to:

```
server {
    listen 443 http2 ssl;
    listen [::]:443 http2 ssl;

    server_name 100.82.20.97; # this should be the IP address used during cert generation

    ssl_certificate /etc/ssl/certs/nginx-selfsigned.crt;
    ssl_certificate_key /etc/ssl/private/nginx-selfsigned.key;
    ssl_dhparam /etc/ssl/certs/dhparam.pem;

   access_log            /var/log/nginx/ssl-sample.log;

    location / {
      proxy_set_header        Host $host;
      proxy_set_header        X-Real-IP $remote_addr;
      proxy_set_header        X-Forwarded-For $proxy_add_x_forwarded_for;
      proxy_set_header        X-Forwarded-Proto $scheme;
      proxy_pass          http://127.0.01:8080;
      proxy_read_timeout  90;
    }
}
```

Save and exit.

## Notes

Since this is a self-signed cert we'll get warnings in the browser.  In my testing Postman didn't work on HTTPS any more due to the errors, this is why I've left the auto redirect off.

There are ways to improve this configuration, but for dev/demo purposes it works.

### Further Investigation / TO DO 
1. Specify 4xx & 5xx error pages.
1. Redirecting from HTTP to HTTPS

    `sudo nano /etc/nginx/default.d/ssl-redirect.conf`

    Update the .conf file to contain: `return 301 https://$host$request_uri/;`

1. HTTP Strict Transport Security / HSTS and HSTS Preloading.
1. [Cipherli.st](https://cipherli.st/) for strong ciphers for NGINX.

