var builder = DistributedApplication.CreateBuilder(args);

var kafka = builder
    .AddKafka("kafka")
    .WithKafkaUI(kafkaUI => kafkaUI.WithHostPort(9100));

builder
    .AddProject<Projects.MyBirds_Server>("server")
    .WithReference(kafka);

builder.Build().Run();