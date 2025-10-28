# Address Book REST API
This project is a REST API application for managing an address book. It was developed as the 2nd assaignment of the "SE4458" course. The API provides endpoints to create, read, update, delete, edit and search for contacts.

## Features
The API supports the following functionalities:
* **List All Addresses:** Retrieve a list of all saved contacts.
* **Get Single Address:** Fetch the details of a specific contact by their ID.
* **Create New Address:** Add a new contact to the address book.
* **Update Address (PUT):** Completely update the information of an existing contact.
* **Edit Address (PATCH):** Partially modify specific fields of an existing contact without changing the rest.
* **Delete Address:** Remove a contact from the address book.
* **Search Addresses:** Search for contacts by first name, last name, city, or district.

## API Endpoints
The following endpoints are available:
| Method | Endpoint | Description |
| :--- | :--- | :--- |
| `GET` | `/api/Addresses` | Retrieves all addresses. |
| `GET` | `/api/Addresses/{id}` | Retrieves a specific address by its ID. |
| `GET` | `/api/Addresses/search` | Searches addresses with a query parameter (e.g., `/api/Addresses/search?query=izmir`). |
| `POST` | `/api/Addresses` | Creates a new address. |
| `PUT` | `/api/Addresses/{id}` | Fully updates an existing address. |
| `PATCH` | `/api/Addresses/{id}`| Partially edits an existing address. |
| `DELETE` | `/api/Addresses/{id}` | Deletes a specific address. |

### Issues
Overall, the development process was smooth. However, a couple of minor challenges were encountered:
1.  **Typo in Model Name:** A minor issue was a typo in the model class name (`Adress` instead of `Address`). This caused several compilation errors throughout the controller until the inconsistency was identified and corrected across all references.
2.  **Understanding REST API Concepts:** Before starting the implementation, some time was dedicated to researching the fundamental concepts of REST APIs, their purpose, and how HTTP methods (`GET`, `POST`, `PUT`, etc.) map to CRUD (Create, Read, Update, Delete) operations. This initial research was crucial for building the API correctly.
