IFS='|' read -ra my_array <<< $COMMIT
echo "::set-env name=COMMIT_TITLE::${my_array[0]}"
echo "::set-env name=COMMIT_URL::${my_array[1]}"
echo "::set-env name=COMMIT_CONTENT::${my_array[2]}"
