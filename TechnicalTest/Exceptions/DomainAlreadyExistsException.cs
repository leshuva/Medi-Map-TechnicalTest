using System;

namespace TechnicalTest.Exceptions;

public class DomainAlreadyExistsException(string message) : Exception(message);