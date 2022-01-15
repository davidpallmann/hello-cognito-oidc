# Hello, Cognito+OIDC!

This is the code project for the [Hello, Cognito+OIDC!](https://davidpallmann.hashnode.dev/hello-cognito-oidc) blog post. 

This episode: Amazon Cognito and OpenID Connect. In this [Hello, Cloud](https://davidpallmann.hashnode.dev/series/hello-cloud) blog series, we're covering the basics of AWS cloud services for newcomers who are .NET developers. If you love C# but are new to AWS, or to this particular service, this should give you a jumpstart.

In this post we'll use Amazon Cognito to secure a "Hello, Cloud" ASP.NET project using OpenID Connect and federated identity with Google. We'll do this step-by-step, making no assumptions other than familiarity with C# and Visual Studio. We're using Visual Studio 2022 and .NET 6.

## Our Hello, Cognito+OIDC Project

We previously introduced Amazon Cognito in Hello, Cognito! and studied one scenario, authenticating against Cognito from an ASP.NET MVC application using the Amazon Cognito Identity Provider. This time, our use case is authenticating to Cognito from an ASP.NET web project using OpenID Connect (OIDC).

We're going to create an ASP.NET MVC web application that authenticates with Cognito using OIDC. Users will be able to sign in with a Cognito identity or a Google identity. The sign-in dialogs will be provided by Cognito and Google. 

See the blog post for the tutorial to create this project and run it on AWS.

