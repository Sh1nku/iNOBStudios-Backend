#!/usr/bin/env python3

# This should be executed as the user with access to the git directory
# Also use visudo to allow starting and stopping the service
# You should
# USER_HERE ALL = NOPASSWD: /usr/sbin/service SERVICE_NAME stop
# USER_HERE ALL = NOPASSWD: /usr/sbin/service SERVICE_NAME start

import subprocess
import argparse

parser = argparse.ArgumentParser()
parser.add_argument('service', help='The service to be stopped and started');
parser.add_argument('tag', help='The tag to be changed to');
args = parser.parse_args()

subprocess.run(['sudo', 'service', args.service, 'stop'])
subprocess.run(['git', 'fetch', '--all', '--tags'], cwd='../')
subprocess.run(['git', 'checkout', 'tags/'+args.tag], cwd='../')
subprocess.run(['dotnet' ,'build', '--configuration', 'Release'], cwd='../')
subprocess.run(['sudo', 'service', args.service, 'start'])