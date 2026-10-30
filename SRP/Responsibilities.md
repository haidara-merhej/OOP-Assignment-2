# Class Responsibilitis Analysis

## WardBoard

### Responsibilities

- Bed and patient assignment.

- Acuity scoring.

- Alert management.

- Handoff note generation.

- Census data export.

**Why This Violates SRP:**
These responsibilities have different reasons to change. Bed assignment rules may change independently of acuity calculations and alert rules. the handoff note format may change without affecting clinical rules, and the census export format may change independently of bed assignment. Keeping these concerns in one class makes changes harder to maintain and increases the risk of effecting unrelated behavior.

## CheckoutBasket

### Responsibilities

- Basket management and total calculation.

- Coupon parsing and discount calculation.

- Gift massage generation.

- Payment authorization.

**Why This Violates SRP:**
These responsibilities have different reasons to change.Basket management and total calculation may change independently of coupon parsing and discount rules. Gift message content may change due to marketing requirements, while payment authorization mechanisms may change independently of basket management. Keeping these concerns in one class makes changes harder to maintain and increases the risk of effecting unrelated behavior.

## SupportTicket

### Responsibilities

- Ticket management.

- Priority detection.

- SLA management.

- Public reply generation.

- Internal escalation message generation.

**Why This Violates SRP:**
These responsibilities have different reasons to change.Ticket management may change independently of priority detection rules. SLA policies may change independently of text analysis, while public reply templates and internal escalation messages may change according to different communication requirements. Keeping these concerns in one class makes changes harder to maintain and increases the risk of effecting unrelated behavior.

## LoanDesk

### Responsibilities

- Loan desk assessment and eligibility.

- Required documents managemnet.

- Decision letter generation.

- Underwriter CSV export.

**Why This Violates SRP:**
These responsibilities have different reasons to change. Risk assessment and eligibility rules may change independently of document requirements. Decision letter wording may change due to legal or communication requirements, while CSV export format may change to meet analytics needs. Keeping these concerns in one class makes changes harder to maintain and increases the risk of effecting unrelated behavior.

## CourseEnrollmentDesk

### Responsibilities

- Enrollment management.

- Welcome packet generation.

- Tuition invoice generation.

**Why This Violates SRP:**
These responsibilities have different reasons to change. Enrollment rules may change independently of welcom message content. Financial regulations, tax rates, and invoice formatting may also change independently of enrollment management. Keeping these concerns in one class makes changes harder to maintain and increases the risk of effecting unrelated behavior.

## KitchenTicket

### Responsibilities

- Kitchen order management.

- Allergen detection.

- Preparation time estimation.

- Thermal ticket rendering.

- Expo lane routing.

**Why This Violates SRP:**
These responsibilities have different reasons to change. Kitchen order management may change independently of allergen detection rules. Preparation time estimation may change as kitchen operation evolve, while allergen rules may change due to regulatory requirements. Thermal ticket formatting may change with printer hardware or layout requirements, and expo lane routing rules may change independently of the other responsibilities. Keeping these concerns in one class makes changes harder to maintain and increases the risk of effecting unrelated behavior.

## SubscriptionBilling

### Responsibilities

- Subscripion billing calculation.

- Invoice number generation.

- Failed payment tracking.

- Dunning email generation.

- Accouting journal export.

**Why This Violates SRP:**
These responsibilities have different reasons to change. Billing calculation rules may change independently of invoice numbering. Failed payment tracking may change independently of email content, while reminder wording and legal requirements may change independently of billing calculations The accouting export format may also change to meet accouting system requirements without affecting billing calculations or email generation. Keeping these concerns in one class makes changes harder to maintain and increases the risk of effecting unrelated behavior.

## WarehousePickList

### Responsibilities

- Stock allocation.

- Walking path ordering.

- Picker instruction generation.

- WMS XML export.

**Why This Violates SRP:**
These responsibilities have different reasons to change. Stock allocation policies may change independently of warehouse path-ordering rules. The wording and presentation of picker instructions may change to improve usability without affecting allocation or path ordering. The XML format may also change to meet requirements of the warehouse management system without affecting the other responsibilities. Keeping these concerns in one class makes changes harder to maintain and increases the risk of effecting unrelated behavior.

## GradeBook

### Responsibilities

- Grade recording and average.

- Letter grade assignment.

- Honor roll eligibility.

- Transcript generation.

- CSV export.

**Why This Violates SRP:**
These responsibilities have different reasons to change. Score recording and average calculation may change independently of grading policies. Letter eligibility criteria may also change independently of each other. Transcript formatting may change to meet institutional document requirements. While the CSV structure may change to satisfy the needs of another system. Keeping these concerns in one class makes changes harder to maintain and increases the risk of effecting unrelated behavior.

## AppointmentDesk

### Responsibilities

- Appointment scheduling and booking.

- Business hours rules.

- Calendar event generation.

- SMS reminder generation.

**Why This Violates SRP:**
These responsibilities have different reasons to change. Business hours and scheduling policies may change independently of calendar event formatting. Calendar integration requirements may change independently of SMS reminder content. While messaging requirements may change without affecting appointment booking. Keeping these concerns in one class makes changes harder to maintain and increases the risk of effecting unrelated behavior.