@rem todo: zip coverity reports 
curl --form token=%coverity_repo_token% \
  --form email=%coverity_email% \
  --form file=@cov_int.zip \
  --form version="Version" \
  --form description="Description" \
  https://scan.coverity.com/builds?project=ddur%2FDBCL