# BankAccountKata


This project is a gRPC Service for a small bank account project.

It uses new feature of .NET 7 .AddJsonTranscoding() and .AddSwagger() .AddSwaggerUI() for a RESTFUL API to enable requests.

The gRPC Service however is not working when the .proto file is imported from another project dependency (which is where is the domain and business logic).
