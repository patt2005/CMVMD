#!/bin/sh
# Asigură-te că directorul de upload există
UPLOAD_PATH="/app/uploads"
if [ ! -d "$UPLOAD_PATH" ]; then
  echo "Creating upload directory at $UPLOAD_PATH..."
  mkdir -p "$UPLOAD_PATH"
fi

# Așteaptă până când baza de date este gata și aplică migrațiile
echo "Waiting for the database to be ready..."
until dotnet ef database update; do
  >&2 echo "Database is unavailable - retrying..."
  sleep 5
done

echo "Database migrations applied successfully. Starting the application..."
exec "$@"