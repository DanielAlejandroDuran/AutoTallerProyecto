using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AutoTaller.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Clientes",
                columns: new[] { "Id", "Email", "FechaRegistro", "Nombre", "Telefono" },
                values: new object[,]
                {
                    { 1, "juan.perez@email.com", new DateTime(2024, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Juan Pérez", "3001234567" },
                    { 2, "maria.lopez@email.com", new DateTime(2024, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "María López", "3009876543" },
                    { 3, "carlos.rodriguez@email.com", new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Carlos Rodríguez", "3005551234" },
                    { 4, "ana.martinez@email.com", new DateTime(2024, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ana Martínez", "3007778888" },
                    { 5, "luis.hernandez@email.com", new DateTime(2024, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Luis Hernández", "3003334444" },
                    { 6, "carmen.silva@email.com", new DateTime(2024, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Carmen Silva", "3006665555" },
                    { 7, "roberto.diaz@email.com", new DateTime(2024, 4, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Roberto Díaz", "3002223333" },
                    { 8, "patricia.ruiz@email.com", new DateTime(2024, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Patricia Ruiz", "3008889999" },
                    { 9, "jorge.castro@email.com", new DateTime(2024, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Jorge Castro", "3004445555" },
                    { 10, "sofia.morales@email.com", new DateTime(2024, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sofía Morales", "3001112222" }
                });

            migrationBuilder.InsertData(
                table: "Repuestos",
                columns: new[] { "Id", "Codigo", "Descripcion", "PrecioUnitario", "Stock", "StockMinimo" },
                values: new object[,]
                {
                    { 1, "FIL-001", "Filtro de Aceite", 15000m, 50, 10 },
                    { 2, "FIL-002", "Filtro de Aire", 25000m, 40, 8 },
                    { 3, "FIL-003", "Filtro de Combustible", 30000m, 35, 7 },
                    { 4, "BUJ-001", "Bujías (set 4)", 45000m, 30, 6 },
                    { 5, "PAST-001", "Pastillas de Freno Delanteras", 80000m, 25, 5 },
                    { 6, "PAST-002", "Pastillas de Freno Traseras", 70000m, 25, 5 },
                    { 7, "DISCO-001", "Discos de Freno Delanteros (par)", 150000m, 20, 4 },
                    { 8, "ACEIT-001", "Aceite Sintético 5W30 (garrafa)", 85000m, 60, 15 },
                    { 9, "ACEIT-002", "Aceite Mineral 20W50 (garrafa)", 55000m, 45, 10 },
                    { 10, "BANDA-001", "Banda de Distribución", 120000m, 18, 4 },
                    { 11, "CORREA-001", "Correa de Accesorios", 35000m, 22, 5 },
                    { 12, "BAT-001", "Batería 12V 45Ah", 280000m, 15, 3 },
                    { 13, "LIQ-001", "Líquido Refrigerante (1L)", 18000m, 50, 10 },
                    { 14, "LIQ-002", "Líquido de Frenos (500ml)", 22000m, 40, 8 },
                    { 15, "LLANTA-001", "Llanta 185/65 R15", 250000m, 16, 4 },
                    { 16, "AMOR-001", "Amortiguador Delantero", 180000m, 12, 3 },
                    { 17, "AMOR-002", "Amortiguador Trasero", 160000m, 12, 3 },
                    { 18, "TERM-001", "Termostato", 45000m, 20, 5 },
                    { 19, "BOMBA-001", "Bomba de Agua", 95000m, 10, 2 },
                    { 20, "EMBRAGUE-001", "Kit de Embrague", 350000m, 8, 2 }
                });

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "Id", "Activo", "Email", "FechaCreacion", "Nombre", "PasswordHash", "Rol" },
                values: new object[,]
                {
                    { 1, true, "admin@autotaller.com", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin Principal", "$2a$11$6p3L0hKfxJ8vQ9xMzN3YPO3L8hKfxJ8vQ9xMzN3", 1 },
                    { 2, true, "mecanico1@autotaller.com", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Carlos Méndez", "$2a$11$6p3L0hKfxJ8vQ9xMzN3YPO3L8hKfxJ8vQ9xMzN3", 2 },
                    { 3, true, "mecanico2@autotaller.com", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Laura Gómez", "$2a$11$6p3L0hKfxJ8vQ9xMzN3YPO3L8hKfxJ8vQ9xMzN3", 2 },
                    { 4, true, "recepcion1@autotaller.com", new DateTime(2024, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ana Torres", "$2a$11$6p3L0hKfxJ8vQ9xMzN3YPO3L8hKfxJ8vQ9xMzN3", 3 },
                    { 5, true, "recepcion2@autotaller.com", new DateTime(2024, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pedro Ramírez", "$2a$11$6p3L0hKfxJ8vQ9xMzN3YPO3L8hKfxJ8vQ9xMzN3", 3 }
                });

            migrationBuilder.InsertData(
                table: "Vehiculos",
                columns: new[] { "Id", "Anio", "ClienteId", "Kilometraje", "Marca", "Modelo", "VIN" },
                values: new object[,]
                {
                    { 1, 2020, 1, 45000, "Toyota", "Corolla", "1HGBH41JXMN109186" },
                    { 2, 2019, 1, 52000, "Honda", "Civic", "2HGFC2F59HH123456" },
                    { 3, 2021, 2, 30000, "Chevrolet", "Spark", "3GCPKSE78HG234567" },
                    { 4, 2020, 3, 40000, "Mazda", "3", "4M2CU87198J345678" },
                    { 5, 2018, 3, 75000, "Nissan", "Sentra", "5N1DR2MM8FC456789" },
                    { 6, 2022, 4, 15000, "Hyundai", "Accent", "6KMHM81BXMU567890" },
                    { 7, 2021, 5, 25000, "Kia", "Rio", "7KNDJ23C08K678901" },
                    { 8, 2019, 6, 60000, "Renault", "Logan", "8LRBG0RB0KN789012" },
                    { 9, 2020, 7, 48000, "Volkswagen", "Gol", "9WVWZZZ1KZW890123" },
                    { 10, 2021, 8, 32000, "Ford", "Fiesta", "1FADP3K28JL901234" },
                    { 11, 2020, 9, 38000, "Suzuki", "Swift", "2SUZUKI96MN012345" },
                    { 12, 2019, 10, 55000, "Mitsubishi", "Mirage", "3MIAGE92LK123456" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Repuestos",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Repuestos",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Repuestos",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Repuestos",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Repuestos",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Repuestos",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Repuestos",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Repuestos",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Repuestos",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Repuestos",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Repuestos",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Repuestos",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Repuestos",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Repuestos",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Repuestos",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Repuestos",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Repuestos",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Repuestos",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Repuestos",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Repuestos",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Vehiculos",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Vehiculos",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Vehiculos",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Vehiculos",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Vehiculos",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Vehiculos",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Vehiculos",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Vehiculos",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Vehiculos",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Vehiculos",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Vehiculos",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Vehiculos",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Clientes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Clientes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Clientes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Clientes",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Clientes",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Clientes",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Clientes",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Clientes",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Clientes",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Clientes",
                keyColumn: "Id",
                keyValue: 10);
        }
    }
}
