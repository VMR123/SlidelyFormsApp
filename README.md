# Slidely Form Application

This is a Windows Forms application built using Visual Basic. It replicates the functionality of Google Forms, allowing users to create and view form submissions.

## Prerequisites

- Visual Studio installed
- Backend server running (refer to https://github.com/VMR123/slidely_backend for setup instructions)

## Getting Started

### Clone the repository:

```bash
git clone https://github.com/VMR123/SlidelyFormApp.git
cd SlidelyFormApp
```

### Open the project in Visual Studio:

1. Launch Visual Studio.
2. Select **Open a project or solution**.
3. Navigate to the `SlidelyFormApp` directory and open the solution file (`SlidelyFormApp.sln`).

### Run the application:

Press `F5` or click on **Start** to run the application.

## Features

### Main Form

- **VIEW SUBMISSIONS (CTRL + V)**: Opens the View Submissions form to navigate through the submissions.
- **CREATE NEW SUBMISSION (CTRL + N)**: Opens the Create Submission form to add a new submission.

### View Submissions Form

- **Name**: Read-only field showing the name from the submission.
- **Email**: Read-only field showing the email from the submission.
- **Phone Num**: Read-only field showing the phone number from the submission.
- **Github Link For Task 2**: Read-only field showing the GitHub link from the submission.
- **Stopwatch time**: Read-only field showing the stopwatch time from the submission.
- **PREVIOUS (CTRL + P)**: Navigates to the previous submission.
- **NEXT (CTRL + N)**: Navigates to the next submission.
- **DELETE (CTRL + D)**: Deletes the current submission.
- **EDIT (CTRL + E)**: Toggles the form into edit mode to allow modifications and then save the updated submission.

### Create Submission Form

- **Name**: Editable field for entering the name.
- **Email**: Editable field for entering the email.
- **Phone Num**: Editable field for entering the phone number.
- **Github Link For Task 2**: Editable field for entering the GitHub link.
- **TOGGLE STOPWATCH (CTRL + T)**: Starts and stops the stopwatch.
- **Stopwatch time**: Label showing the current stopwatch time.
- **SUBMIT (CTRL + S)**: Submits the form to the backend server.

### Keyboard Shortcuts

#### Main Form:
- **View Submissions**: `CTRL + V`
- **Create New Submission**: `CTRL + N`

#### View Submissions Form:
- **Previous**: `CTRL + P`
- **Next**: `CTRL + N`
- **Delete**: `CTRL + D`
- **Edit**: `CTRL + E`

#### Create Submission Form:
- **Toggle Stopwatch**: `CTRL + T`
- **Submit**: `CTRL + S`

## Additional Features

- **Edit Submission**: Allows users to edit the existing submissions.
- **Delete Submission**: Allows users to delete the existing submissions.

With these features, you can now view, edit, and delete submissions, enhancing the overall functionality of the Slidely Form Application.
