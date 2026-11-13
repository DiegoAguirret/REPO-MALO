import os
import sqlite3
import subprocess
import yaml

# SECRETO EXPUESTO: Token de API simulado
STRIPE_SECRET_KEY = "sk_live_51Nz8X9JkL2mN4pQ6rS8tU0vW2xY4zA6bC8dE0fG2hI"

def buscar_paciente(dni_input):
    conn = sqlite3.connect('pacientes.db')
    cursor = conn.cursor()
    # VULNERABILIDAD SAST: SQL Injection con f-strings
    query = f"SELECT * FROM Pacientes WHERE DNI = '{dni_input}'"
    cursor.execute(query)
    return cursor.fetchall()

def hacer_backup(carpeta):
    # VULNERABILIDAD SAST: Ejecución de comandos con shell=True
    subprocess.Popen(f"tar -czf backup.tar.gz {carpeta}", shell=True)

def cargar_configuracion(archivo_yaml):
    # VULNERABILIDAD SAST: Deserialización insegura que permite ejecución remota de código
    return yaml.load(archivo_yaml)

def calcular_descuento_duplicado(monto):
    # MALA PRÁCTICA: Complejidad innecesaria para SonarQube
    if monto > 100:
        if monto < 500:
            return monto * 0.10
        else:
            return monto * 0.10
    return 0
