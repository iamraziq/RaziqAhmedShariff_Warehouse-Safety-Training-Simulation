# Warehouse-Safety-Training-Simulation by Raziq Ahmed Shariff

**Try the WebGL Build Here:**  
[Warehouse Safety Training Simulation - SCORM Cloud](https://app.cloud.scorm.com/sc/InvitationConfirmEmail?publicInvitationId=a31daf2b-4f9f-496c-b4d6-bb27f998942d)

---

## Overview

The **Warehouse Safety Training Simulation** is an interactive SCORM-based Unity project designed for LMS integration.
It simulates a virtual training environment where learners must inspect and handle warehouse items according to safety guidelines.
The simulation tracks user progress and completion status through **SCORM 1.2** standards.

---

## Common Systems and Features

### SCORM Implementation Summary

This project implements **SCORM 1.2** for LMS integration.

* **SCORMManager.cs** – Handles communication with the LMS through SCORM API calls.
* **SCORMWrapper.js** – JavaScript bridge between Unity WebGL build and LMS.
* **SCORMProgressHandler.cs** – Updates learner progress and completion percentage to SCORM.
* **imsmanifest.xml** – Defines SCORM metadata and references `index_scorm.html`.
* **index_scorm.html** – Wrapper HTML file loading the Unity WebGL build.

---

### Learner Data Handling

* Fetches learner name and ID from LMS.
* Displays learner name on welcome screen and certificate.
* Updates progress accross scenes.
* Tracks completion and pass status (complete/incomplete shown in LMS).

---

## Build Instructions

### Exporting a SCORM Package

Before building, go to **Build Settings > Player Settings > Resolution and Presentation > WebGL Template** and select the **SCORM Template**, which includes the `SCORMWrapper.js`, `index_scorm.html`, and other necessary files for proper SCORM functionality in WebGL builds.

In Unity, navigate to:  
**Tools > SCORM Packaging > Export SCORM Package**

Configure the following settings in the dialog:

**SCORM Configuration**  
- **Identifier:** (e.g. `UnitySCORMTest`)  
- **Title:** (e.g. `Unity WebGL SCORM Test`)  

**Hosting Option**  
- *Host build on external server?* → If yes, set the external URL.

**File Settings**  
- **Index SCORM File:** (e.g. `index_scorm.html`)  
- **JS Wrapper File:** (e.g. `SCORMWrapper.js`)  

**Options**  
- *Open folder after export?* → If checked, the exported folder opens automatically.

Click **Build and Export SCORM Package** to:

1. Build the Unity WebGL project.  
2. Generate SCORM manifest (`imsmanifest.xml`).  
3. Include SCORM wrapper and HTML files.  
4. Create a ZIP package ready for LMS upload.  

> **Note:**  
> `ScormExporterAdvanced` is a custom Unity Editor script that automates SCORM packaging for this project.  
> It generates all necessary XML, wrapper, and manifest files to produce a SCORM-compliant ZIP package compatible with most LMS systems.

---

## Git Workflow / Collaboration

### Git Workflow

**Branches:**

* `main` – Stable, production-ready branch.
* `develop` – Active development branch.
* `feature/scene-classroom` – Scene 1: Classroom-related development.
* `feature/scene-warehouse` – Scene 2: Warehouse-related development.
* `feature/scorm-integration` – SCORM and LMS-related work.
* `code-cleanup` – Refactoring and maintenance.

### Collaboration

Currently a single-developer repository.
Commits follow a feature-based branching strategy.

---

## Known Issues / Limitations

* The build is intended primarily for SCORM-based LMS deployment and not optimized for standalone desktop execution.
* All existing functionalities work as intended under current requirements.

---

## Scene 1: Classroom Safety Briefing

### Scene Description

Scene 1 introduces the learner to the basic safety concepts.
It includes interactive items, safety video, and checklists to complete before moving to the warehouse scene.

**Objectives:**

* Learn fundamental warehouse safety principles through classroom-based guidance.  
* Understand the purpose and use of key warehouse items (tape gun, barcode scanner, gloves, safety goggles, etc.).  
* Inspect each item interactively using hover, selection, and rotation features.  
* Complete the inspection checklist to mark items as inspected.  
* Progress to the next scene upon completing the checklist, with SCORM progress recorded.

### Scene 1 Scripts and Their Responsibilities

1. **ChecklistManager** – Manages the checklist items displayed on-screen. Tracks task completion and ensures learners complete it before progressing.
2. **ClassroomManager** – Controls the overall flow of the classroom scene, including learner details, and UI updates.
3. **InteractableItem** – Represents objects that the learner can interact with. Handles hover effects, highlighting, and triggers OnSelect event.
4. **InteractionManager** – Central system for managing interactions between the player and interactable objects.
5. **Interactor** – Attached to the player object. Sends raycasts to detect interactable items in the scene and triggers InteractionManager methods.
6. **ItemInspector** – Handles item inspection behavior when the learner selects an object.
7. **ProgressBar** – Displays the learner’s progress for Scene 1. Reflects completion of inspecting items and updates checklist.
8. **SoundManager** – A global singleton used across the entire project. Controls audio playback and sound effects.
9. **TrayZone** – Defines the placement area where items must be dropped after inspection. Triggers events when picked items are placed and updates the progress.
10. **VideoLoader** – Loads and plays video content from the StreamingAssets folder dynamically at runtime. Displays a loading panel until the video is fully prepared.
11. **SCORMManager** – Singleton instance initialized in Scene 1 and persists across all scenes. Handles SCORM communication (Initialize, Commit, Terminate), manages learner data, and updates course completion status.
12. **SCORMProgressHandler** – Present in each scene to track and send scene-specific progress updates to the `SCORMManager`, ensuring accurate reporting to the LMS.

## Scene Flow Summary

1. Learner enters the classroom and is welcomed by the guiding avatar.  
2. A safety video automatically plays on the projector screen.  
3. Learner inspects items on the table; each item glows on hover and can be rotated for inspection.  
4. Inspection checklist is filled out corresponding to each item.  
5. Upon completing all checklist items, SCORM progress for Scene 1 is updated.  
6. Learner is prompted to proceed to the next scene.

---

### Scene 2: Warehouse Simulation & Quiz

### Scene Description

Scene 2 transitions the learner from classroom concepts to hands-on warehouse safety simulation.
The learner must locate and collect safety-related items and correctly place them on a table within a 5-minute time limit.

**Objectives:**

* Collect items (Tape Gun, Barcode Scanner, Helmet, Gloves) placed randomly around the warehouse.
* Place all collected items on the table within 5 minutes.
* Follow a guiding avatar providing step-by-step instructions.
* Track overall training progress through a visible progress bar.
* Complete a 3-question quiz after task completion.
* Pass with at least 2 correct answers to receive a completion certificate.

### Scene 2 Scripts and Their Responsibilities

1. **WarehouseManager** – Core controller for Scene 2. Handles task timer, item placement validation, progress updates, and transitions to the quiz panel.
2. **PlayerController** – Controls player movement (WASD + mouse rotation) for navigating the warehouse.
3. **PickupItem** – Manages item interaction logic, allowing the player to pick up and carry items to the table.
4. **ProgressBar** – Displays overall progress of the scene. Updates dynamically as items are placed.
5. **QuizManager** – Handles quiz logic, answer validation, and pass/fail feedback. Displays retry prompt if learner fails.
6. **CertificateManager** – Displays a completion certificate upon successful quiz completion. Integrates with SCORM for final course completion status.
7. **SoundManager** – Provides audio guidance for item pickups, placements, quiz, and win/lose moments.
8. **SCORMProgressHandler** – Present in each scene to track and send scene-specific progress updates to the `SCORMManager`, ensuring accurate reporting to the LMS.

### Scene Flow Summary

1. Learner enters the warehouse and is introduced by the guiding avatar.
2. Timer starts (5:00 minutes visible on-screen).
3. Learner collects all required items and places them on the table.
4. On successful placement of all items, the quiz appears.
5. If 2 out of 3 questions are answered correctly, the learner passes and receives a completion certificate.
6. Certificate completion triggers SCORM “Course Complete” status update to the LMS.

---

## Credits

Developed by: **Raziq Ahmed Shariff**
Role: **Senior Unity Developer**
Contact: **[raziqshariff18@gmail.com](mailto:raziqshariff18@gmail.com)**
