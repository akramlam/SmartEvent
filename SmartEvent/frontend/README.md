# SmartEvent Frontend

This is the frontend application for the SmartEvent platform, built with React.

## Prerequisites

- Node.js (v14.x or later)
- npm (v6.x or later)

## Getting Started

1. Install dependencies:
   ```
   npm install
   ```

2. Start the development server:
   ```
   npm start
   ```

3. Open [http://localhost:3000](http://localhost:3000) to view it in the browser.

## Features

- View list of events
- View event details
- Register for events

## API Integration

The frontend communicates with the SmartEvent API. Make sure the API is running at the URL specified in the `proxy` field in `package.json`.

## Project Structure

- `/src/components` - Reusable UI components
- `/src/pages` - Page components
- `/src/services` - API service functions 