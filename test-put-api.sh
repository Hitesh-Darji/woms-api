# Test Script for Stock Request API
# This script tests the PUT API with concurrency handling

# Test the PUT API with the corrected payload
curl -X PUT "https://localhost:7001/api/StockRequest/test/B9498AEB-7DBA-4734-BEF3-8086585BE57" \
  -H "Content-Type: application/json" \
  -d '{
    "notes": "Updated stock request - urgent maintenance work",
    "requestItems": [
      {
        "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        "itemId": "0AC65261-FF0A-43B2-B1A8-378F11C0D746",
        "requestedQuantity": 10,
        "notes": "Updated quantity for maintenance",
        "orderIndex": 0
      }
    ]
  }'

# Expected responses:
# 200 OK - Success with updated stock request
# 409 Conflict - Concurrency conflict (data modified by another user)
# 404 Not Found - Stock request or request item not found
# 400 Bad Request - Validation error or other issue
