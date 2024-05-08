using System;

namespace TechnicalTest.Exceptions;

public class DatabaseErrorException(string message) : Exception(message);