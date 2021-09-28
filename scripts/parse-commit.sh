IFS='|' read -ra my_array <<< $COMMIT
echo "COMMIT_TITLE=${my_array[0]}" >> $GITHUB_ENV
echo "COMMIT_URL=${my_array[1]}" >> $GITHUB_ENV
echo "COMMIT_CONTENT=${my_array[2]}" >> $GITHUB_ENV
