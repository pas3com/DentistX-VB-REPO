# API Summary

## WhatsApp API Endpoints

---

### 1. Get QR Code for Pairing

**Endpoint:** `/api/whatsapp/qr/:clinicId`  
**Method:** GET  
**Parameters:**

- `clinicId` (path)

**Response:**

- `{ success, qrCode? }` – QR code string if available
- `{ success, status, message }` – Alternative status message
- `{ success: false, error }` – Error details

---

### 2. Initiate WhatsApp Connection

**Endpoint:** `/connect/:clinicId`  
**Method:** GET  
**Parameters:**

- `clinicId` (path)

**Response:**

- `{ success, message }` – Connection status
- `{ success: false, error }` – Error message

---

### 3. Enqueue a Message for Sending

**Endpoint:** `/api/whatsapp/send`  
**Method:** POST  
**Body (JSON):**

```json
{
  "clinicId": "clinic1",
  "number": "970512345678",
  "message": "your message"
}
```

**Response:**

- `{ status: "queued", messageId }` – Message successfully enqueued
- `{ success: false, error }` – Error details

---

### 4. Get WhatsApp Connection Status

**Endpoint:** `/api/whatsapp/status/:clinicId`  
**Method:** GET  
**Parameters:**

- `clinicId` (path)

**Response:**

- `{ connected: true/false }`

---

### 5. List Pending Messages in Queue

**Endpoint:** `/api/whatsapp/queue/:clinicId`  
**Method:** GET  
**Parameters:**

- `clinicId` (path)

**Response:**

```json
{
  "clinicId": "clinic1",
  "pendingMessages": [
    {
      "messageId": "id1",
      "clinicId": "clinic1",
      "number": "970512345678",
      "message": "text",
      "etaSeconds": 12
    }
  ]
}
```

---

### 6. Delete Message from Queue

**Endpoint:** `/api/whatsapp/queue/:clinicId/:messageId`  
**Method:** DELETE  
**Parameters:**

- `clinicId` (path)
- `messageId` (path)

**Response:**

- `{ success: true }` – Message deleted
- `{ success: false, error }` – Error details

---

### 7. Get Failed Messages

**Endpoint:** `/api/whatsapp/failed-messages/:clinicId`  
**Method:** GET  
**Parameters:**

- `clinicId` (path)

**Response:**

```json
{
  "clinicId": "clinic1",
  "failedMessages": [
    {
      "clinicId": "clinic1",
      "number": "970512345678",
      "message": "text",
      "error": "error details",
      "failedAt": "2024-06-08T12:34:56Z"
    }
  ]
}
```

---

### 8. Retry All Failed Messages

**Endpoint:** `/api/whatsapp/retry-failed/:clinicId`  
**Method:** POST  
**Parameters:**

- `clinicId` (path)

**Response:**

- `{ status: "retried", count }` – Number of messages retried
