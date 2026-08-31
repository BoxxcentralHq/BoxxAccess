# Security

- Keep the terminal on a trusted local network; never expose its SDK port to the public internet.
- Store device communication passwords and external API credentials only in local protected configuration.
- Use a dedicated service identity for external API access and restrict it to the minimum required permissions.
- Treat face templates and event photos as sensitive biometric data. Collect, retain, export, and delete them only under an approved policy and applicable law.
- Log administrative actions and access-policy changes, but never log secrets or biometric templates.
