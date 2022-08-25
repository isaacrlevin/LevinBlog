---
date: 2017-08-18T11:15:58-04:00
tags: ["angular", "javascript"]
title: "JWT Interceptor with Angular HttpClient"
---

## HttpClient and Interceptors

<br />

Last month, the Angular team released version 4.3 of their SPA Framework. One of the big additions in this release was the upgrade to the Http mechanism used by the framework, now called HttpClient. The process to make Http calls is different now, and by initial smell for the better. To adjulation from the community, the return of HttpInterceptors, a mechanism that sits between your application and your Api. What they do is capture the request and transform it to your liking and capture the response and transform it to your liking. Doing this in a uniform manner is helpful if you do something for all your Api calls.

<br />

## Read more on Http Interceptors at the [Angular.io site](https://angular.io/guide/http#intercepting-all-requests-or-responses)

<br />

## Can I wrap my calls in Auth?

<br />

One thing that is often needed when writing web applications is making http calls that are authorized (like writing a blog post for instance). In the past, we could create a service and inject a JWT token obtained somewhere else in the application(login method) in the request. That works but now there is a better way, Http Interceptors. Here is a gist of an Interceptor that attaches a JWT token to all POST/PUT/DELETE Http requests

<br />

```ts
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';

@Injectable()
export class JWTInterceptor implements HttpInterceptor {

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        if (req.method === 'DELETE' || req.method === 'POST' || req.method === 'PUT') {
            let currentUser = JSON.parse(localStorage.getItem('currentUser'));
            if (currentUser && currentUser.token) {
                req = req.clone({
                    setHeaders: {
                        authorization: 'Bearer ' + currentUser.token
                    }
                });
            }
        }
        return next.handle(req);
    }
}
```

<br />

With this, we now have a standardized way of injecting tokens into web requests, and the api call in our Angular service does not even care, how cool is that!