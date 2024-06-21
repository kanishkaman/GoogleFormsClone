# Backend Server

## Description
This is the backend server built with TypeScript and Express. It handles form submissions and retrieves submitted data.

## Installation

1. Install dependencies:
    ```sh
    npm install
    ```

2. Compile TypeScript:
    ```sh
    npx tsc
    ```

3. Start the server:
    ```sh
    node dist/index.js
    ```

## Endpoints

- **`GET /ping`**: Returns `true`.
- **`POST /submit`**: Submits a form with the following parameters:
  - `name`
  - `email`
  - `phone`
  - `github_link`
  - `stopwatch_time`
- **`GET /read`**: Reads a submission by index (query parameter `index`).
