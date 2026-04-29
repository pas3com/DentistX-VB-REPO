# Create Customer Endpoint

## Endpoint Path

`/api/customers`

## HTTP Method
Private ReadOnly _baseUrl As String = "https://whatsapp-sender-api.dentistx.net"
`POST`


## Inputs

### Headers

| Header          | Required | Description                              |
|-----------------|----------|------------------------------------------|
| Content-Type    | Yes      | Must be `application/json`               |


### Body (JSON)

| Field         | Type   | Required | Description                              |
|---------------|--------|----------|------------------------------------------|
| clinicId      | guid   | Yes      | Clinic unique identifier                 |
| clinicNameEn  | string | Yes      | Clinic name in English (max 200 chars)   |
| clinicNameAr  | string | Yes      | Clinic name in Arabic (max 200 chars)    |
| drNameEn      | string | Yes      | Doctor name in English (max 200 chars)   |
| drNameAr      | string | Yes      | Doctor name in Arabic (max 200 chars)    |
| specialistEn  | string | No       | Specialist in English (max 200 chars)    |
| specialistAr  | string | No       | Specialist in Arabic (max 200 chars)      |
| addressEn     | string | No       | Address in English (max 500 chars)       |
| addressAr     | string | No       | Address in Arabic (max 500 chars)        |
| phone         | string | No       | Phone number (max 50 chars)              |
| mobile        | string | No       | Mobile number (max 50 chars)             |
| email         | string | No       | Email address (max 200 chars)            |
| clinicLogo    | string | No       | Clinic logo URL or base64 (max 2000 chars) |

**Example Request Body:**

```json
{
  "clinicId": "a1b2c3d4-e5f6-7890-abcd-ef1234567890",
  "clinicNameEn": "Dental Care Clinic",
  "clinicNameAr": "عيادة العناية بالأسنان",
  "drNameEn": "Dr. Ahmed Ali",
  "drNameAr": "د. أحمد علي",
  "specialistEn": "General Dentistry",
  "specialistAr": "طب الأسنان العام",
  "addressEn": "123 Main St",
  "addressAr": "شارع الرئيسي 123",
  "phone": "+1234567890",
  "mobile": "+0987654321",
  "email": "clinic@example.com",
  "clinicLogo": null
}
```

## JSON Outputs

### Success Response (201 Created)

```json
{
  "id": "a1b2c3d4-e5f6-7890-abcd-ef1234567890",
  "clinicNameEn": "Dental Care Clinic",
  "clinicNameAr": "عيادة العناية بالأسنان",
  "drNameEn": "Dr. Ahmed Ali",
  "drNameAr": "د. أحمد علي",
  "specialistEn": "General Dentistry",
  "specialistAr": "طب الأسنان العام",
  "addressEn": "123 Main St",
  "addressAr": "شارع الرئيسي 123",
  "phone": "+1234567890",
  "mobile": "+0987654321",
  "email": "clinic@example.com",
  "clinicLogo": null,
  "createdAt": "2025-03-08T10:00:00Z"
}
```

| Field        | Type   | Description                              |
|--------------|--------|------------------------------------------|
| id           | string | Created customer/clinic unique ID        |
| clinicNameEn | string | Clinic name in English                   |
| clinicNameAr | string | Clinic name in Arabic                    |
| drNameEn     | string | Doctor name in English                   |
| drNameAr     | string | Doctor name in Arabic                    |
| specialistEn | string | Specialist in English (optional)        |
| specialistAr | string | Specialist in Arabic (optional)          |
| addressEn    | string | Address in English (optional)            |
| addressAr    | string | Address in Arabic (optional)             |
| phone        | string | Phone number (optional)                  |
| mobile       | string | Mobile number (optional)                 |
| email        | string | Email address (optional)                 |
| clinicLogo   | string | Clinic logo (optional)                    |
| createdAt    | string | ISO 8601 datetime of creation           |

## Expected Errors / Status Codes

| Status Code | Condition                          | Response Body Example                    |
|-------------|------------------------------------|------------------------------------------|
| 400 Bad Request | Validation failed (missing/invalid fields) | Model validation errors                |
| 403 Forbidden | Valid token but user is not Admin | `{ "message": "Forbidden" }`             |
