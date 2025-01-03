# Signing Commits in Git/GitHub

Add Git's `bin` to `PATH` within your operating system so that `gpg` is globally available.

```
C:\Program Files\Git\usr\bin\
```

In CLI, generate a key via `gpg`:

```
gpg --full-generate-key
```

It'll walk you through several steps:

- Select `(1) RSA and RSA` for the kind of key
- Enter `4096` for bit size
- Expiration can be whatever desired
- When entering real information, the name and comment can be anything, but **e-mail must match GitHub's e-mails you're
  using via the `user.email` git config setting**
- Confirm the above
- GPG will prompt for a passphrase that you'll have to enter once per session to use the key
  - This is optional. In the prompt, just continue leaving the box blank. It'll warn and ask for confirmation. Accept
    all prompts to continue.

Here's what the output was when I made mine (this key has been deleted and is expired, so it's safe to share):

```
We need to generate a lot of random bytes. It is a good idea to perform
some other action (type on the keyboard, move the mouse, utilize the
disks) during the prime generation; this gives the random number
generator a better chance to gain enough entropy.
We need to generate a lot of random bytes. It is a good idea to perform
some other action (type on the keyboard, move the mouse, utilize the
disks) during the prime generation; this gives the random number
generator a better chance to gain enough entropy.
gpg: /path/to/.gnupg/trustdb.gpg: trustdb created
gpg: directory '/path/to/.gnupg/openpgp-revocs.d' created
gpg: revocation certificate stored as '/path/to/.gnupg/openpgp-revocs.d/A2FE4621EE707551CA002097DEFCEADDEB4CD823.rev'
public and secret key created and signed.

pub   rsa4096 2025-01-02 [SC] [expires: 2025-01-03]
      A2FE4621EE707551CA002097DEFCEADDEB4CD823
uid                      Scott DePouw (NimblePros) <scott.depouw@nimblepros.com>
sub   rsa4096 2025-01-02 [E] [expires: 2025-01-03]
```

Take the public key portion and export it:

```
gpg --armor --export A2FE4621EE707551CA002097DEFCEADDEB4CD823
```

Results in the following public key block, that GitHub will need:

```
-----BEGIN PGP PUBLIC KEY BLOCK-----

mQINBGd2bIcBEAC10v7P1rTyg13bbuqCwYy4LUgYT3jhOowVygXN2PVG2FcybkAF
/RIJnrW61gP5piG9drSZRTTcI19m7IuYM0nLr5IPbdOpKFkMgWlPxlfFDtx18nRj
[removed the bulk for brevity]
prXJz6aRecUrLYMub6NUVFWL1e9iHMcTy4nrO0rCfceprA==
=o4fM
-----END PGP PUBLIC KEY BLOCK-----
```

On GitHub, go to your [Keys Settings](https://github.com/settings/keys) and click "New GPG Key" (green button), pasting
in your key and leaving a friendly Name.

Once there, Git can be configured to sign commits with your new key

- Inform Git where the GPG executable is:

```
git config --global gpg.program "C:\Program Files\Git\usr\bin\gpg.exe"
```

- The signing key must be added to your git configuration. You can add it to `--global` if you only have one account,
or this can be configured per-repository.
  - You can technically use the e-mail address instead of the key, but I like using the key
  - To find the key, list them from `gpg`

```
gpg --list-secret-keys --keyid-format=long
```

This should result in a listing:

```
gpg: checking the trustdb
gpg: marginals needed: 3  completes needed: 1  trust model: pgp
gpg: depth: 0  valid:   1  signed:   0  trust: 0-, 0q, 0n, 0m, 0f, 1u
gpg: next trustdb check due at 2025-01-03
[keyboxd]
---------
sec   rsa4096/DEFCEADDEB4CD823 2025-01-02 [SC] [expires: 2025-01-03]
      A2FE4621EE707551CA002097DEFCEADDEB4CD823
uid                 [ultimate] Scott DePouw (NimblePros) <scott.depouw@nimblepros.com>
ssb   rsa4096/6C6719AD663F40D1 2025-01-02 [E] [expires: 2025-01-03]
```

- Configure Git with the Key (you can use either `DEF...` (the "sec", or secret key) or `6C6...` (the "ssb", or secret
  subkey) for these commands):

```
git config user.signingkey DEFCEADDEB4CD823
```

Aside: If you want to delete the above key from GPG, use the following:

```
gpg --delete-secret-key DEFCEADDEB4CD823
```

Now whenever you commit, pass the `-S` flag in order to Sign your commit. For the first time in your session, GPG will
prompt you for the passphrase (if it has one) you created when making the key. These commits should show as "verified"
in GitHub:

```
git add README.md
git commit -m "Updated README with notes" -S
git push
```

To instruct Git to always sign commits (so you don't have to remember `-S` every time), configure (add `--global` for
every repository):

```
git config commit.gpgsign true
```

The same can be done for Tags:

```
git config tag.gpgsign true
```

If you have multiple e-mail addresses, repeat the process above for each one, and configure Git with its particular
`signingkey` as appropriate.

### References

- [GitHub Docs - Managing Commit Signature Verification](https://docs.github.com/en/authentication/managing-commit-signature-verification)
- [GitHub Docs - Generating a New GPG Key](https://docs.github.com/en/authentication/managing-commit-signature-verification/generating-a-new-gpg-key)
- [GitHub Docs - Signing Commits](https://docs.github.com/en/authentication/managing-commit-signature-verification/signing-commits)
- [Gordon Beeming (YouTube) - Setting Up Commit Signature Verification for GitHub](https://www.youtube.com/watch?v=Y8WgCNLJaRA)
