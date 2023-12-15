global using System.Data.Common;
global using Npgsql;
global using System.Runtime.Serialization;
global using Dapper;
global using FluentValidation;
global using MediatR;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.EntityFrameworkCore;
global using Ordering.API;
global using Ordering.API.Commands;
global using IntegrationEvents;
global using IntegrationEvents.Model;
global using Ordering.API.Validations;
global using Ordering.Domain.Aggregates.Buyer;
global using Ordering.Domain.Aggregates.Order;
global using Ordering.Domain.Events;
global using Ordering.Domain.Exceptions;
global using Ordering.Domain.SeedWork;
global using Ordering.Infrastructure;
global using Ordering.Infrastructure.Repositories;
global using Microsoft.Extensions.Options;
global using Polly;
global using Polly.Retry;
global using ServiceDefaults;
global using Swashbuckle.AspNetCore.SwaggerGen;